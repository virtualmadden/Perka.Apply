using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Perka.Apply.Client.Adapters
{
    public interface IGithubApiAdapter
    {
        Task<string> GetOrdersAsync();
    }

    public class GithubAdapter : HttpClientAdapterBase, IGithubApiAdapter
    {
        private readonly string _endpoint = ApplicationSettingsAdapter.ApplicationSettings.GithubApi.Uri;

        public GithubAdapter() : this(null)
        {
        }

        private GithubAdapter(HttpMessageHandler handler) : base(handler)
        {
        }

        public async Task<string> GetOrdersAsync()
        {
            try
            {
                return await HandleResponse(await GetAsync(new Uri(_endpoint)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void SetHttpHeaders()
        {
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("Perka.Apply.Client/1.0");
        }
    }
}