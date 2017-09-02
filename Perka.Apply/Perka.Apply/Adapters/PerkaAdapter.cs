using System.Net.Http.Headers;

namespace Perka.Apply.Adapters
{
    internal class PerkaAdapter : HttpClientAdapterBase
    {
        protected override void SetHttpHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
        }
    }
}