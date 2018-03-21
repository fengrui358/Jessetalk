using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Token的颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token的颁发对象
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string SecretKey => "HelloKey";
    }
}
