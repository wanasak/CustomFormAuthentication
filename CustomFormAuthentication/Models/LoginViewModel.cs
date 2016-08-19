using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFormAuthentication.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.", AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
