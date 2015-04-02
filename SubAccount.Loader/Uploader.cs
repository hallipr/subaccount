namespace SubAccount.Loader
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SubAccount.Common;

    internal class Uploader
    {
        private readonly Config config;

        public Uploader(Config config)
        {
            this.config = config;
        }

        public async Task UploadTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            var client = new HttpClient
            {
                BaseAddress = this.config.ServiceAddress
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var contentString = JsonConvert.SerializeObject(transactions);

            var content = new StringContent(contentString, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("transactions", content);

            response.EnsureSuccessStatusCode();
        }
    }
}