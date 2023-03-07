using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebBanVeMayBay.Areas.Admin.Model
{
    public class Login
    {
            [Required(ErrorMessage = "Bạn chưa nhập Account")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Bạn chưa nhập Password")]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
    }
}
