using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpGet("createToken")]
        public Task<string> CreateToken()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("create token error");

            return _authenticationService.CreateToken();
        }

        [HttpPost("createSession")]
        public Task<string> CreateSession([FromBody] CreateSession token)
        {
            return _authenticationService.CreateSession(token);
        }
    }
}
