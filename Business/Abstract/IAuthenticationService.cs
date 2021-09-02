﻿using Business.Models;
using Business.Responses;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthenticationService
    {
        bool Register(Users user);
        User Authenticate(User user);
        Task<string> CreateToken();
        Task<string> CreateSession(CreateSession token);
        bool ValidationEmail(ValidationEmail validationEmail);
    }
}
