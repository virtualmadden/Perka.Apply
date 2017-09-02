using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Perka.Apply.Adapters
{
    internal class GithubAdapter : HttpClientAdapterBase
    {
        protected override void SetHttpHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
        }
    }
}