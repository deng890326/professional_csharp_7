using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BooksServiceClientSample.Services
{
    abstract public class HttpClientService<T> : IDisposable
        where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService<T>> _logger;
        public HttpClientService(ILogger<HttpClientService<T>> logger)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(UrlService.BaseAddress),
            };
            _logger = logger;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _disposed = true;
        }

        public virtual async Task<T?> GetAsync<TId>(string requestUri, TId id)
        {
            string json = await GetInternalAsync($"{requestUri}{id}");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string requestUri)
        {
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json)
                ?? Enumerable.Empty<T>();
        }

        private async Task<string> GetInternalAsync(string requestUri)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(_httpClient));
            HttpResponseMessage resp = await _httpClient.GetAsync(requestUri);
            LogInfo($"status from GET {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }

        private void LogInfo(string message,
            [CallerMemberName] string callerName = "")
        {
            _logger.LogInformation($"{nameof(HttpClientService<T>)}.{callerName}: {message}.");
        }

        public virtual async Task<T?> Post(string requestUri, T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_disposed) throw new ObjectDisposedException(nameof(_httpClient));

            var resp = await _httpClient.PostAsJsonAsync(requestUri, item);
            LogInfo($"status from POST {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            LogInfo($"added resource at {resp.Headers.Location}");
            string json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public virtual async Task Put<TId>(string requestUri, TId id, T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_disposed) throw new ObjectDisposedException(nameof(_httpClient));

            var resp = await _httpClient.PutAsJsonAsync($"{requestUri}{id}", item);
            LogInfo($"status from PUT {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
        }

        public virtual async Task Delete<TId>(string requestUri, TId id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (_disposed) throw new ObjectDisposedException(nameof(_httpClient));

            var resp = await _httpClient.DeleteAsync($"{requestUri}{id}");
            LogInfo($"status from DELETE {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
        }

        private bool _disposed = false;
    }
}
