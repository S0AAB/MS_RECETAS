using System.Threading.Tasks;

public interface IPersonaServiceAPI
{
    Task<dynamic> ObtenerPersona(int id);
}