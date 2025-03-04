using Autofac;
using Autofac.Integration.Mvc;
using MS_RECETAS.App_Start;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace MS_RECETAS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
   
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.Register();

            var receptorMQ = DependencyResolver.Current.GetService<ReceptorMQ>();

            if (receptorMQ != null)
            {
                Task.Run(() => receptorMQ.Escucha());
            }
            else
            {
                throw new Exception("Error: No se pudo resolver la dependencia de ReceptorMQ en Autofac.");
            }
        }
    }
}
