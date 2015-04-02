namespace SubAccount.Loader
{
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    internal class Config
    {
        public AccountConfig[] Accounts { get; set; }

        [JsonIgnore]
        public string Source { get; set; }

        public static Config Load(string path)
        {
            var config = File.Exists(path)
                ? JsonConvert.DeserializeObject<Config>(File.ReadAllText(path))
                : new Config {Accounts = new AccountConfig[0] };

            config.Source = path;

            return config;
        }

        public void Save()
        {
            Save(this.Source);
        }

        public void Save(string path)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonText = JsonConvert.SerializeObject(this, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText(path, jsonText);
        }

        public System.Uri ServiceAddress { get; set; }
    }
}