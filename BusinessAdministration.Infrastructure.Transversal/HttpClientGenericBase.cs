using BusinessAdministration.Aplication.Dto.Base;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using BusinessAdministration.Infrastructure.Transversal.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Transversal
{
    public abstract class HttpClientGenericBase<T> : IHttpClientGenericBase<T> where T : DataTransferObject
    {
        protected abstract string Controller { get; }

        private readonly HttpClient _client;
        private readonly string baseUrl;

        public HttpClientGenericBase(
            HttpClient client,
            IOptions<HttpClientSettings> settings)
        {
            _client = client ?? throw new ClientNotEspecificateException();
            if (settings.Value.GetServiceUrl() == null) throw new UriFormatException();
            baseUrl = settings.Value.GetServiceUrl().ToString();
        }

        public async Task<IEnumerable<T>> Get(string action)
        {
            ValidateNotNullPath(Controller);
            var response = await _client.GetAsync($"{baseUrl}{Controller}/{action}").ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
        public async Task<T> Post(T request, string action)
        {
            ValidateNotNullPath(Controller);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PostAsync($"{baseUrl}{Controller}/{action}", stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
        public async Task<T> Put(T request, string action)
        {
            ValidateNotNullPath(Controller);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PutAsync($"{baseUrl}{Controller}/{action}", stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<T> Patch(T request, string action)
        {
            ValidateNotNullPath(Controller);
            var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.PatchAsync($"{baseUrl}{Controller}/{action}", stringRequest).ConfigureAwait(false);
            ValidateUserUnauthorized(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }


        public async Task<T> Delete(string action)
        {
            //Todo: Michael, review method
            ValidateNotNullPath(Controller);
            var response = await _client.DeleteAsync($"{baseUrl}{Controller}/{action}").ConfigureAwait(false);
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
