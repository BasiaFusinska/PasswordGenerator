using System.Web;
using System.Web.Http;

namespace PasswordGeneratorWeb
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
