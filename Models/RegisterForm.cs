using System;

namespace LoginApp.Models 
{
    public class RegisterForm
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
        public string Regex { get; set; }
    }
}