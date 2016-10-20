using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClonestoneMVC.Models
{
    public class UserRegistration
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gamertag { get; set; }
        public string Email { get; set; }
        public string EmailValidation { get; set; }
        public string Password { get; set; }
        public string PasswordValidation { get; set; }


    }
}