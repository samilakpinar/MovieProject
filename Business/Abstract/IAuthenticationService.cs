﻿using Business.Models;
using Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthenticationService
    {
        Task<string> CreateToken();
        Task<string> CreateSession(CreateSession token);
        bool ValidationEmail(ValidationEmail validationEmail);
    }
}
