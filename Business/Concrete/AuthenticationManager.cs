using Business.Abstract;
using Business.Models;
using Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthenticationManager: IAuthenticationService
    {

        HttpClient httpClient = new HttpClient();
        private readonly AppSettings _appSettings;

        public AuthenticationManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<string> CreateToken()
        {
            var url = $"{_appSettings.Url}authentication/token/new?api_key=a87921328b55114d690b35cec33d3aae";
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateSession(CreateSession token)
        {
            var url = $"{_appSettings.Url}authentication/session/new?api_key=a87921328b55114d690b35cec33d3aae";
            var json = System.Text.Json.JsonSerializer.Serialize(token);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
