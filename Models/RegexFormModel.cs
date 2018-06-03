using System;

namespace LoginApp.Models 
{
    public class RegexForm
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool checkMinLength { get; set; }
        public int minLength { get; set; }
        public bool checkMaxLength { get; set; }
        public int maxLength { get; set; }
        public bool checkUppercase { get; set; }
        public int minUppercase { get; set; }
        public bool checkLowercase { get; set; }
        public int minLowercase { get; set; }
        public bool checkSpecialSigns { get; set; }
        public int minSpecialSigns { get; set; }
        public bool checkDigits { get; set; }
        public int minDigits { get; set; }
    }
}