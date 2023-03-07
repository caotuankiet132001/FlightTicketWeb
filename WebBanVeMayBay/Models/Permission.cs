using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanVeMayBay.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string GroupId { get; set; }
        public int RoleId { get; set; }
    }
}