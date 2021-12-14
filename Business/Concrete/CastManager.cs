using Business.Abstract;
using Business.Models;
using Business.Responses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CastManager : ICastService
    {
        private readonly AppSettings _appSettings;

        public CastManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<Cast>> GetPopulerCast(int movieId)
        {
            if (movieId < 0)
            {
                return null;
            }

            var url = $"{_appSettings.Url}movie/{movieId}/credits?api_key={_appSettings.ApiKey}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var castModel = JsonConvert.DeserializeObject<PopulerCastResponse>(jsonAsString);
            return castModel.Cast;

        }


    }
}
