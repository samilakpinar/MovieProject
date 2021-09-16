using Business.Abstract;
using Business.Models;
using Business.Responses;
using Entities.Concrete;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        //Not Appsettings olayının IConfiguration ile çözümüdür
        HttpClient httpClient = new HttpClient();
        private readonly IConfiguration config;
        private readonly IUserService _userService;

        public AuthenticationManager(IConfiguration configuration, IUserService userService)
        {
            config = configuration;
            _userService = userService;
        }

        public bool Register(Users user)
        {
            if (user.Email.Length == 0 || user.Password.Length < 8 || user.Name.Length == 0 || user.Surname.Length == 0)
            {
                return false;
            }

            var result = _userService.Add(user);

            return result.Status;


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


        public async Task<string> CreateToken()
        {

            string _settings = config.GetConnectionString("ConnectionString");

            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/token/new?api_key={apiKey}";
            var response = await httpClient.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return result;



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


            return await response.Content.ReadAsStringAsync();

        }

        public async Task<SessionWithLoginResponse> CreateSessionWithLogin(SessionWithLogin sessionLogin)
        {
            //session için url linkleri için geliştirme yapılamsı sağlanacak.

            string httpUrl = config.GetSection("AppSettings").GetSection("Url").Value;
            string apiKey = config.GetSection("AppSettings").GetSection("ApiKey").Value;

            var url = $"{httpUrl}authentication/token/validate_with_login?api_key={apiKey}";
            var json = System.Text.Json.JsonSerializer.Serialize(sessionLogin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            var jsonAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SessionWithLoginResponse>(jsonAsString);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return result;

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

        public bool ResetPassword(string email)
        {
            //checkEmail
            if (email == null)
            {
                return false;
            }

            var findUser = _userService.GetByEmail(email);

            if (findUser == null)
            {
                return false;
            }


            //create email message
            var emailSend = new MimeMessage();
            emailSend.From.Add(MailboxAddress.Parse("samilakpinar8@gmail.com"));
            emailSend.To.Add(MailboxAddress.Parse(email));
            emailSend.Subject = "Reset Password";

            //email md5 hash function
            var verify = MD5Hash(email);

            var url = "http://localhost:4200/reset/" + email + "/" + verify;

            emailSend.Body = new TextPart(TextFormat.Html) { Text = "Reset Password: " + url };

            //send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);


            try
            {
                smtp.Authenticate("samilakpinar8@gmail.com", "Youtube1");
                smtp.Send(emailSend);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

                Console.WriteLine("message gitmedi " + ex.Message);
                return false;
            }


            return true;


        }

        //md5 for reset password
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //metnin boyutundan hash hesaplar
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //hesapladıktan sonra hashi alır
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //her baytı 2 hexadecimal hane olarak değiştirir
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public bool UpdatePassword(ResetPassword reset)
        {

            if (reset.Password == null || reset.Email == null)
            {
                return false;
            }

            var findUser = _userService.GetByEmail(reset.Email);

            if (findUser == null)
            {
                return false;
            }

            //User password encryp and decryp
            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<SecurityManager>(LockerKey);
            string encryptKey = locker.Encrypt(reset.Password);


            findUser.Password = encryptKey;

            var updatePassword = _userService.Update(findUser);

            if (updatePassword.Status)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
