using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcSample.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 是否是学生
        /// </summary>
        public bool IsStudent { get; set; }

        /// <summary>
        /// 是否是好学生
        /// </summary>
        public bool? IsGoodStudent { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public string Address { get; set; }
    }
}
