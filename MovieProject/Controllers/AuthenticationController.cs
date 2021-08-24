using Business.Abstract;
using Business.Models;
using Business.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// <param name="user"></param>
        /// <returns>BaseResponse</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public BaseResponse<User> Authenticate([FromBody] User user)
        {
            
            BaseResponse<User> response = new BaseResponse<User>();

            var findUser = _authenticationService.UserLogin(user);

            if(findUser == null)
            {
                response.Data = null;
                response.ErrorMessages = ".";
                return response;
            }

            var token = _jwtAuthenticationService.Authenticate(findUser);
            if(token == null)
            {
                //response.Data = Unauthorized();
                response.Data = null;
                response.ErrorMessages = "izin reddedildi";
                return response;
            }

            findUser.Token = token; //oluşturulan token değeri kullanıcıya eklendi
            findUser.Password = null; //password null yapıldı.

            response.Data = findUser;
            response.ErrorMessages = null;
            return response; //user değeri döndürülür.

        }

        /// <summary>
        /// Create film token
        /// </summary>
        /// <returns>BaseResponse</returns>
        [AllowAnonymous]
        [HttpGet("create-token")]
        public async Task<string> CreateToken()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            //logger.Info("create token error");

            //Base response error
            return await _authenticationService.CreateToken();
        }

        /// <summary>
        /// User create session after film token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>String session value</returns>
        [AllowAnonymous]
        [HttpPost("create-session")]
        public Task<string> CreateSession([FromBody] CreateSession token)
        {
            return _authenticationService.CreateSession(token);
        }

        /// <summary>
        /// User validation email for create session
        /// </summary>
        /// <param name="validationEmail"></param>
        /// <returns>Boolean</returns>
        [AllowAnonymous]
        [HttpPost("validation-email")]
        public BaseResponse<bool>  CheckEmail([FromBody] ValidationEmail validationEmail )
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            response.Data = _authenticationService.ValidationEmail(validationEmail);
            response.ErrorMessages = null;

            return response;
        }

    }
}
