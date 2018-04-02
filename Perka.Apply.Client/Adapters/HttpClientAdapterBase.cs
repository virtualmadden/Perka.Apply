using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Perka.Apply.Client.Adapters
{
    public abstract class HttpClientAdapterBase : IDisposable
    {
        internal const string ContentType = "application/json";

        private static readonly List<HttpStatusCode> IoExceptionStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.GatewayTimeout,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable
        };

        private readonly HttpMessageHandler _handler;
        internal HttpClient Client;

        protected HttpClientAdapterBase(HttpMessageHandler handler)
        {
            InitializeClient();
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

        protected async Task<string> HandleResponse(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "returned no response.");

            var content = response.Content != null ? await response.Content.ReadAsStringAsync() : null;

            if (response.IsSuccessStatusCode)
                return content;

            HandleFailedResponse(response.StatusCode, response.RequestMessage?.RequestUri, content);

            return content;
        }

        private static void HandleFailedResponse(HttpStatusCode statusCode, Uri requestUri, string error)
        {
            if (IoExceptionStatusCodes.Contains(statusCode))
                throw new IOException($"Http exception: {statusCode} Reason: {error}");

            throw new Exception($"Unhandled response from {requestUri}.\n" + $"Http message failure. {Environment.NewLine} Status Code: {statusCode} {Environment.NewLine} Reason: {error}");
        }
    }
}