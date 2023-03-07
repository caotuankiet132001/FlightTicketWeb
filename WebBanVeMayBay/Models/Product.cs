using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanVeMayBay.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CatId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Detail { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double Pricesale { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
        //public DateTime Time { get; set; }
        public string MidAir { get; set; }
        public int BreakTime { get; set; }
        public string PlanTo { get; set; }
        public string PlanFrom { get; set; }
        public int Seat1 { get; set; }
        public int Seat2 { get; set; }
        public int OnAir { get; set; }
        //public DateTime Date { get; set; }
        public DateTime DateTime { get; set; }
        
        public string Date => DateTime.ToString("dd/MM/yyyy");
        
        public string Time => DateTime.ToString("hh:mm tt");

    }
}