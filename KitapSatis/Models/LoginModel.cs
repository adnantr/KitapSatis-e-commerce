﻿using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class LoginModel
    {
        [Required]
       public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string Email { get; internal set; }
    }
}
