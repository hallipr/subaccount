namespace SubAccount
{
    using System.Web.Configuration;
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SubAccount.Models;

    internal static class ContainerConfig
    {
        public static Container Configure()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static void InitializeContainer(Container container)
        {
            container.RegisterSingle(InitializeConfig());
            container.RegisterWebApiRequest<IDataContext, DataContext>();
            container.RegisterWebApiRequest<IDataStore, DataStore>();
        }

        private static Config InitializeConfig()
        {
            return new Config
            {
                ConnectionString = WebConfigurationManager.ConnectionStrings["Default"].ConnectionString
            };
        }
    }
}