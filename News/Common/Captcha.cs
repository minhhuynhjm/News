using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Common
{
    public static class Captcha
    {
        /// <summary>
        /// Recaptcha v3
        /// </summary>
        public const string GoogleRecaptchaSecretKey_v3 = "6Lf_x78UAAAAAHGWwijqM1BKRtjZ96Y44a-vaIZw";
        public const string GoogleRecaptchaSiteKey_v3 = "6Lf_x78UAAAAABvf2OWjPClZGTgLJdAbO3V7PA4H";

        /// <summary>
        /// Recaptcha v2
        /// </summary>
        public const string GoogleRecaptchaSiteKey_v2 = "6Ld99L8UAAAAAHuOh4ywInGeh3p4DMh1F586NHZp";
        public const string GoogleRecaptchaSecretKey_v2 = "6Ld99L8UAAAAALOkhBG7bjuvjZp80YA5AhURdIF3";

    }
}