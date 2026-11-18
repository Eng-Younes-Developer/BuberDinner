using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BuberDinner.infrastructure.Authintication
{
    public class JwtSetting
    {
        public const string SectionName = "JwtSetting";
        public string Secret { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

    }
}
