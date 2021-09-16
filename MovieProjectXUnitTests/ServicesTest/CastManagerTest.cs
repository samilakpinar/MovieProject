using Business.Abstract;
using Business.Models;
using Business.Responses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace MovieProjectXUnitTests.ServicesTest
{
    public class CastManagerTest : ICastService
    {
        private readonly AppSettings _appSettings;


        public CastManagerTest(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public CastManagerTest()
        {

        }

        public async Task<List<Cast>> GetPopulerCast(int movieId)
        {
            if (movieId.ToString().Length < 3)
            {
                return null;
            }

            var url = $"{_appSettings.Url}movie/{movieId}/credits?api_key={_appSettings.ApiKey}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            PopulerCastResponse castModel = JsonConvert.DeserializeObject<PopulerCastResponse>(jsonAsString);
            return castModel.Cast;
        }
    }
}
