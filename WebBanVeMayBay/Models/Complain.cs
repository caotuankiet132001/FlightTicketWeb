using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebBanVeMayBay.Models
{
    [Table("Complains")]
    public class Complain
    {
        [Key]
        public int Id { get; set; } //Mã phiếu khiếu nại
        public string Type { get; set; } //Loại khiếu nại
        public string Detail { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateSend { get; set; }
        public int status { get; set; }
    }
}