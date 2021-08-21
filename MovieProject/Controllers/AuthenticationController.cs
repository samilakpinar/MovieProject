using Business.Abstract;
using Business.Models;
using Business.Responses;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;
        private IJwtAuthenticationService _jwtAuthenticationService;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtAuthenticationService jwtAuthenticationService)
        {
            _authenticationService = authenticationService;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        /// <summary>
        /// login with email and password
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public BaseResponse<string> Authenticate([FromBody] User user)
        {
            //3- kullanıcı burada oluşacak. frontend tarafından email, şifre, session id ve boş bir token değeri gelecek.
            //login diye bir tane class oluştur. User diye bir tane class oluştur.
            //kullanıcı return olarak email, token değeri ve session değeri döner

            // servise email ve şifre gönderilecel

            BaseResponse<string> response = new BaseResponse<string>();

            var findUser = _authenticationService.UserLogin(user);

            if(findUser == null)
            {
                response.Data = "";
                response.ErrorMessages = "Kullanıcı bulunamadı.";
                return response;
            }

            var token = _jwtAuthenticationService.Authenticate(findUser);
            if(token == null)
            {
                response.Data = Unauthorized().ToString();
                response.ErrorMessages = "izin reddedildi";
                return response;
            }

            response.Data = token;
            response.ErrorMessages = null;
            return response; //token değeri döndürülür.

        }

        /// <summary>
        /// create token
        /// </summary>
        /// <returns></returns>
        [HttpGet("create-token")]
        public Task<string> CreateToken()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("create token error");

            return _authenticationService.CreateToken();
        }

      

        [HttpPost("create-session")]
        public Task<string> CreateSession([FromBody] CreateSession token)
        {
            return _authenticationService.CreateSession(token);
        }

        [HttpPost("validation-email")]
        public bool CheckEmail([FromBody] ValidationEmail validationEmail )
        {
            return _authenticationService.ValidationEmail(validationEmail);
        }

    }
}
