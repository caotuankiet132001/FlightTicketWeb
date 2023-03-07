using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace WebBanVeMayBay.Models
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        public int Id { get; set; } //Mã hóa đơn
        public int TicketId { get; set; } //Mã vé
        public int PlanId { get; set; } //Mã chuyến bay
        public string PlanName { get; set; } //Tên chuyến bay
        [Required]
        public string ClientId { get; set; } //Cmnd khách hàng
        [Required]
        public string ClientName { get; set; } //Tên khách hàng
        public string PhoneNumber { get; set; } //Sdt
        public string Date { get; set; } //Ngày bay
        public string Time { get; set; } //Giờ bay
        public int Price { get; set; } //Giá
    }
}