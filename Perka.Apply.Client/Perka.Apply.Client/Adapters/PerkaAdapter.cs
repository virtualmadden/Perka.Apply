using System.Net.Http.Headers;

namespace Perka.Apply.Client.Adapters
{
    internal class PerkaAdapter : HttpClientAdapterBase
    {
        protected override void SetHttpHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
        }
    }
}