using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auth
{
    class AuthDto
    {
        public  UserDto User { get; set; }

        public string token { get; set; }
    }
}
