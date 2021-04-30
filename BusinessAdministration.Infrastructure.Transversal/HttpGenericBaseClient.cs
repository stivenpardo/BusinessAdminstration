using BusinessAdministration.Aplication.Dto.Base;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using BusinessAdministration.Infrastructure.Transversal.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Transversal
{
    public class HttpGenericBaseClient : IHttpGenericBaseClient
    {
        private readonly HttpClient _client;
        private readonly string urlBase;

        public HttpGenericBaseClient(
            HttpClient client,
            IOptions<HttpClientSettings> settings)
        {
            _client = client ?? throw new ClientNotEspecificateException();
            if (settings.Value.GetServiceUrl() == null) throw new UriFormatException();
            urlBase = settings.Value.GetServiceUrl().ToString();
            //_client.BaseAddress = settings.Value.GetServiceUrl();
        }
        public async Task<T> Get<T>(string path) where T : class
        {
            ValidateNotNullPath(path);
            var response = await _client.GetAsync($"{urlBase}{path}").ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<TResponse> Patch<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject
        {
            ValidateNotNullPath(path);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PatchAsync(path, stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<TResponse> Post<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject
        {
            ValidateNotNullPath(path);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PostAsync(path, stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<TResponse> Put<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject
        {
            ValidateNotNullPath(path);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PutAsync(path, stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
        public async Task<T> Delete<T>(string path) where T : DataTransferObject
        {
            ValidateNotNullPath(path);
            var response = await _client.DeleteAsync(path).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        #region Validations
        private static void ValidateNotNullPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new UriIsNullOrEmptyException();
        }
        private static void ValidateUserUnauthorized(HttpResponseMessage response)
        {
            if (string.Equals(response.StatusCode.ToString(), "unauthorized", StringComparison.OrdinalIgnoreCase))
                throw new UserUnauthorizedException();
        }
        #endregion
    }
}
