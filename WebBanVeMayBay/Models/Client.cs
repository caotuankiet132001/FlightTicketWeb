using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanVeMayBay.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; } //CMND KH
        public string ClientName { get; set; } //HoTenKH
        public string ClientAddress { get; set; } //Diachi KH
        public string ClientAccount { get; set; }
        public string ClientPassword { get; set; }
        public string ClientSex { get; set; }
        public string ClientEmail { get; set; }
        public string PhoneNumber { get; set; }
        public int Point { get; set; }
        public int RoleId { get; set; }
        public string NewPassword { get; set; }
    }
}