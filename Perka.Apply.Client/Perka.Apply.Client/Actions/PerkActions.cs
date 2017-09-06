using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Actions
{
    public interface IPerkaActions
    {
        Task<PerkaApplicationResponse> PostApplication(PerkaApplicationRequest applicationRequest);
    }

    public class PerkaActions : IPerkaActions
    {
        private readonly IPerkaApiAdapter _perkaApiAdapter;

        public PerkaActions(IPerkaApiAdapter perkaApiAdapter = null)
        {
            _perkaApiAdapter = perkaApiAdapter ?? new PerkaApiAdapter();
        }

        public async Task<PerkaApplicationResponse> PostApplication(PerkaApplicationRequest applicationRequest)
        {
            try
            {
                var content = JsonConvert.SerializeObject(applicationRequest);

                var response = await _perkaApiAdapter.PostApplicationAsync(content);

                return JsonConvert.DeserializeObject<PerkaApplicationResponse>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}