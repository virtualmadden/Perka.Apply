using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Perka.Apply.Adapters
{
    internal abstract class HttpClientAdapterBase : IDisposable
    {
        internal const string ContentType = "application/json";

        private readonly HttpMessageHandler _handler;
        internal HttpClient Client;

        protected HttpClientAdapterBase() : this(null)
        {
        }

        protected HttpClientAdapterBase(HttpMessageHandler handler)
        {
            _handler = handler;
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

        protected abstract void SetHttpHeaders();

        private void InitializeClient()
        {
            if (_handler == null)
            {
#if DEBUG
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
#endif

                Client = new HttpClient();
            }
            else
            {
                Client = new HttpClient(_handler);
            }

            Client.Timeout = new TimeSpan(0, 0, 30);
        }

        private void InitializeHeaders(bool useHeaders)
        {
            if (useHeaders)
                SetHttpHeaders();
            else
                InitializeClient();
        }

        protected async Task<HttpResponseMessage> GetAsync(Uri uri, bool setHeaders = true)
        {
            InitializeHeaders(setHeaders);

            try
            {
                return await Client.GetAsync(uri);
            }
            catch (TaskCanceledException exception)
            {
                throw new TimeoutException("HTTP operation timed out.", exception);
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content, bool setHeaders = true)
        {
            InitializeHeaders(setHeaders);

            try
            {
                return await Client.PostAsync(uri, content);
            }
            catch (TaskCanceledException exception)
            {
                throw new TimeoutException("HTTP operation timed out.", exception);
            }
        }
    }
}