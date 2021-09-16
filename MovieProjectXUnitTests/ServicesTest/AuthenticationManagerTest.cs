using Business.Abstract;
using Business.Models;
using Business.Responses;
using Entities.Concrete;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MovieProjectXUnitTests.ServicesTest
{
    public class AuthenticationManagerTest : IAuthenticationService
    {

        HttpClient httpClient = new HttpClient();
        private readonly IConfiguration config;
        private readonly IUserService _userService;

        public AuthenticationManagerTest(IConfiguration configuration, IUserService userService)
        {
            config = configuration;
            _userService = userService;
        }

        public AuthenticationManagerTest()
        {

        }



        public User Authenticate(User user)
        {
            if (user.Email.Length == 0 || user.Password.Length < 8)
            {
                return null;
            }

            var findUser = _userService.GetByEmailAndPassword(user.Email, user.Password);

            if (findUser == null)
            {
                return null;
            }

            User userModel = new User();
            userModel.Name = findUser.Name;
            userModel.Surname = findUser.Surname;
            userModel.Email = findUser.Email;
            userModel.Password = findUser.Password;
            userModel.Permisson = findUser.Permisson;
            userModel.Token = "";
            userModel.Password = null;

            return userModel;
        }

        public async Task<string> CreateSession(CreateSession token)
        {

            if (token.request_token == "")
            {
                return null;
            }

            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/session/new?api_key={apiKey}";
            var json = System.Text.Json.JsonSerializer.Serialize(token);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);


            return await response.Content.ReadAsStringAsync(); ;
        }

        public async Task<string> CreateToken()
        {
            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/token/new?api_key={apiKey}";
            var response = await httpClient.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public bool Register(Users user)
        {
            if (user.Email.Length == 0 || user.Password.Length < 8 || user.Name.Length == 0 || user.Surname.Length == 0)
            {
                return false;
            }

            try
            {
                _userService.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidationEmail(ValidationEmail validationEmail)
        {
            if (validationEmail.Email.Length == 0 || validationEmail.Token.Length == 0)
            {
                return false;
            }

            //create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("samilakpinar8@gmail.com"));
            email.To.Add(MailboxAddress.Parse(validationEmail.Email));
            email.Subject = "Validation Email";


            var url = "https://www.themoviedb.org/authenticate/" + validationEmail.Token + "/allow";

            email.Body = new TextPart(TextFormat.Html) { Text = "Validation Email: " + url + " " };

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

        public Task<SessionWithLoginResponse> CreateSessionWithLogin(SessionWithLogin sessionLogin)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassword(ResetPassword reset)
        {
            throw new NotImplementedException();
        }
    }
}
