using Business.Abstract;
using Business.Models;
using Business.Responses;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
    
        private static HttpClient httpClient = new HttpClient();
        private readonly IConfiguration config;
        private readonly IUserService _userService;

        public AuthenticationManager(IConfiguration configuration, IUserService userService)
        {
            config = configuration;
            _userService = userService;
        }

        public bool Register(Users user)
        {
            if (user.Password.Length < 8)
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

        public bool ResetPassword(string email)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            if (email == "" || email == null)
            {
                return false;
            }

            var findUser = _userService.GetByEmail(email);

            if (findUser == null)
            {
                return false;
            }

            //email md5 hash function
            var verify = MD5Hash(email);




            //send Grid structre for azure.
            var client = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("samil5807@hotmail.com", "Movie-Trailer"),
                Subject = "Reset Password",
                HtmlContent = "<strong>Dear " + findUser.Name + ",</strong><br><p>Please enter the link for reset password: https://trailer-movies.netlify.app/reset/" + email + "/" + verify + "</p>"

            };
            msg.AddTo(new EmailAddress(email, "Test user"));
            var response = client.SendEmailAsync(msg);


            return true;

            //send mail for localhost
            /*
            logger.Info("email veritabanı sorgualama:" + findUser.Email);


            var fromAddress = new MailAddress("samilakpinar8@gmail.com");
            var fromPassword = "Youtube1";
            var toAddress = new MailAddress(email);

            logger.Info("md5 hashleme giriş");
            //email md5 hash function
            var verify = MD5Hash(email);

            string subject = "Reset Password";
            var url = "https://trailer-movies.netlify.app/reset/" + email + "/" + verify;

            string body = "Reset Password:" + url; // new TextPart(TextFormat.Html) { Text = "Reset Password: " + url };

            logger.Info("System.Net.Mail.SmtpClient smtp neweleme yapılması");

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            logger.Info("MailMessage new işlemi");

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                logger.Info("aa");
                smtp.Send(message);
            }



            logger.Info("fonkaiyon çıkışı");
            return true;

            */
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
