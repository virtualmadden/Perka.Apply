using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Actions
{
    public interface IGithubActions
    {
        Task<List<GithubProjectResponse>> GetProjects();
    }

    public class GithubActions : IGithubActions
    {
        private readonly IGithubApiAdapter _githubApiAdapter;

        public GithubActions(IGithubApiAdapter githubAdapter = null)
        {
            _githubApiAdapter = githubAdapter ?? new GithubAdapter();
        }

        public async Task<List<GithubProjectResponse>> GetProjects()
        {
            try
            {
                var response = await _githubApiAdapter.GetOrdersAsync();

                return JsonConvert.DeserializeObject<List<GithubProjectResponse>>(response, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}