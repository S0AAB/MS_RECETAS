
using MS_RECETAS.Infrastructure.Repositories;
using MS_RECETAS.Application.Implementation;
using MS_RECETAS.Domain.Interfaces;
using MS_RECETAS.Domain.Models;
using MS_RECETAS.Application.ServicesInterfaces;

using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using AutoMapper;
using MS_RECETAS.Application.Mappings;


namespace MS_RECETAS.App_Start
{
	public class AutofacConfig
	{

        public static void Register()
        {
            var builder = new ContainerBuilder();

            //Registro del contexto
            builder.RegisterType<RecetasDBEntities>().InstancePerLifetimeScope();

            // Registrar Repositorio y Servicio
            builder.RegisterType<RecetaService>().As<IRecetaService>().InstancePerLifetimeScope();
            builder.RegisterType<RecetaRepository>().As<IRecetasRepository>().InstancePerLifetimeScope();

            //Registro servicio API CITAS

            builder.RegisterType<PersonaServiceAPI>().SingleInstance();
            //RabbitMQ
            builder.RegisterType<ReceptorMQ>().AsSelf().SingleInstance();

            // Configurar Web API
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Configuracion automapper
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RecetaMapping());
                cfg.AddProfile(new EstadoRecetaMapping());
            })).AsSelf().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}