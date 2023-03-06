using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConfiguration.Console
{
    internal interface IUserService
    {
        string GetDefaultPassword();
    }


    internal class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GetDefaultPassword()
        {
            return _configuration["Users:DefaultPassword"]!;

        }
    }
}
