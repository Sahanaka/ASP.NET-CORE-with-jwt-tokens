using Microsoft.AspNetCore.Mvc;
using JWTTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTest.Services
{
    public interface IJWTService
    {
        public string GenerateJWTtoken(User userInfo);
    }
}