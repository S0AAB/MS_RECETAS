using RabbitMQ.Client;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using MS_RECETAS.Application.ServicesInterfaces;
using System.Diagnostics;
using MS_RECETAS.Application.Dto;


public class ReceptorMQ
{
    private const string QueueName = "recetas_queue";
    private const string ExchangeName = "recetas_exchange";
    private readonly IRecetaService _recetaService;

    public ReceptorMQ(IRecetaService recetaService) {
        _recetaService = recetaService;
    }


    public async Task Escucha()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Fanout, durable: true);
        //durable: Sobrevivi al reinicio del brocker
        //exclusie: Pdora ser utilizada por otras conexiones
        //autoDelete:La borra se borrara cuando el consumidor este abajo
        await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueBindAsync(QueueName, ExchangeName, "");


        Debug.WriteLine("---- Esperando mensaje.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var receta = JsonConvert.DeserializeObject<RecetaDto>(message);


            Debug.WriteLine($"Recibido: {message}");

          

            _recetaService.Create(receta);

            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: QueueName, autoAck: true, consumer: consumer);
    }
}
