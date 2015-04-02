namespace SubAccount
{
    using System.Linq;
    using System.Web.Http;
    using SubAccount.Common;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerConfig.Configure();
        }
    }
}
