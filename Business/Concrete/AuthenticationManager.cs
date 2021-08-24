using Business.Abstract;
using Business.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Business.Responses;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Business.Concrete
{
    public class AuthenticationManager: IAuthenticationService
    {
        //Not Appsettings olayının IConfiguration ile çözümüdür
        HttpClient httpClient = new HttpClient();
        private readonly IConfiguration config;

        public AuthenticationManager(IConfiguration configuration)
        {
            config = configuration;
        }

        
        public async Task<string> CreateToken()
        {
            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/token/new?api_key={apiKey}";
            var response = await httpClient.GetAsync(url);
            
            return await response.Content.ReadAsStringAsync();


        }

       
        public async Task<string> CreateSession(CreateSession token)
        {

            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/session/new?api_key={apiKey}";
            var json = System.Text.Json.JsonSerializer.Serialize(token);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
            
        }

        
        public bool ValidationEmail(ValidationEmail validationEmail)
        {

            //create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("samilakpinar8@gmail.com"));
            email.To.Add(MailboxAddress.Parse(validationEmail.Email));
            email.Subject = "Validation Email";

            var url = "https://www.themoviedb.org/authenticate/"+validationEmail.Token;

            email.Body = new TextPart(TextFormat.Html) { Text = "Validation Email: "+url+" " };

            //send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);


            try
            {
                smtp.Authenticate("samilakpinar8@gmail.com", "Youtube1");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

                Console.WriteLine("message gitmedi " + ex.Message);
                return false;
            }

            return true;
        }

        public User UserLogin(User user)
        {
            //loginden email ve şifre gelecek

            List<User> users = new List<User>();
            users.Add(new User(1, "Şamil", "Akpınar", "samilakpinar6@gmail.com", "12345", "", "", 1));
            users.Add(new User(2, "Ahmet", "Uluçay", "ahmet@gmail.com", "12345", "", "", 2));
            users.Add(new User(3, "Ali", "Tepe", "ali@gmail.com", "12345", "", "", 3));

            var findUser = users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            if(findUser == null)
            {
                return null;
            }

            //şuan kullanıcı elimizde var ve token üretmeyi bekliyor.
            
            //oluşan user için token oluştur ve session oluştur.
            return findUser;
        }

        
    }
}
