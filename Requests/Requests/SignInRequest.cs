﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Requests
{
    [Serializable]
    public class SignInRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return $"Username: {Username} Password: {Password}";
        }
    }
}
