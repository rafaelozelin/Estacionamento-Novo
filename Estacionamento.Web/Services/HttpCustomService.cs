using Estacionamento.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Estacionamento.Web.Services
{
    public class HttpCustomService : IHttpCustomService
    {
        public async Task<HttpResponseMessage> RequestGetAsync(string request)
        {
            var client = GetClient(request);

            return await client.GetAsync(request);
        }

        public async Task<HttpResponseMessage> RequestDeleteAsync(string request)
        {
            var client = GetClient(request);

            return await client.DeleteAsync(request);
        }

        public async Task<HttpResponseMessage> RequestPostAsync(string requestUri, object objeto)
        {
            var response = new HttpResponseMessage();

            try
            {
                var client = GetClient(requestUri);
                var content = ObjectToStringContent(objeto);

                response = await client.PostAsync(requestUri, content);
            }
            catch
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<HttpResponseMessage> RequestPutAsync(string requestUri, object objeto)
        {
            var response = new HttpResponseMessage();
            try
            {
                var client = GetClient(requestUri);
                StringContent content = ObjectToStringContent(objeto);

                response = await client.PutAsync(requestUri, content);
            }
            catch
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        private static StringContent ObjectToStringContent(object objeto) => new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
        
        private static HttpClient GetClient(string baseUrl)
        {
            var client = new HttpClient() { BaseAddress = new Uri(baseUrl) };

            return client;
        }
    }
}
