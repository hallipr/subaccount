namespace SubAccount
{
    using System.Web.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            SetJsonSerializerSettings(config.Formatters.JsonFormatter.SerializerSettings);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void SetJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            settings.Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = true,
                CamelCaseText = true
            });
        }
    }
}
