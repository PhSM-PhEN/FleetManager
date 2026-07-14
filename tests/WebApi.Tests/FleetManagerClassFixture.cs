using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests
{
    public class FleetManagerClassFixture(CustomWebApplicationFactory customWebApplication) : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient = customWebApplication.CreateClient();

        protected async Task<HttpResponseMessage> DoPost(string requestUri, object request,
            string token = "")
        {
            AuthorizeRequest(token);

            return await _httpClient.PostAsJsonAsync(requestUri, request);
        }

        protected async Task<HttpResponseMessage> DoGet(string requestUri, string token = "")
        {
            AuthorizeRequest(token);


            return await _httpClient.GetAsync(requestUri);
        }
        protected async Task<HttpResponseMessage> DoDelete(string requestUri, string token = "")
        {
            AuthorizeRequest(token);

            return await _httpClient.DeleteAsync(requestUri);
        }
        protected async Task<HttpResponseMessage> DoPut(string requestUri, object request, string token = "")
        {
            AuthorizeRequest(token);

            return await _httpClient.PutAsJsonAsync(requestUri, request);
        }

        protected async Task<HttpResponseMessage> DoPatch(string requestUri, string token = "")
        {
            AuthorizeRequest(token);

            return await _httpClient.PatchAsync(requestUri, null);
        }


        private void AuthorizeRequest(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (string.IsNullOrEmpty(token))
                return;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}