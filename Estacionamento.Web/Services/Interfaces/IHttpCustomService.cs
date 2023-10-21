namespace Estacionamento.Web.Services.Interfaces
{
    public interface IHttpCustomService
    {
        public Task<HttpResponseMessage> RequestGetAsync(string request);
        public Task<HttpResponseMessage> RequestDeleteAsync(string request);
        Task<HttpResponseMessage> RequestPostAsync(string requestUri, object objeto);
        Task<HttpResponseMessage> RequestPutAsync(string requestUri, object objeto);
    }
}
