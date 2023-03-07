using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace WebBanVeMayBay.Models
{
    public class ProductCategory
    {

        public int Id { get; set; }
        public int CatId { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public string Detail { get; set; }

        public string MetaKey { get; set; }

        public string MetaDes { get; set; }
        public string Img { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double Pricesale { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
        public string CatName { get; set; }
        public DateTime DateTime { get; set; }
        public string MidAir { get; set; }
        public int BreakTime { get; set; }
        public string PlanTo { get; set; }
        public string PlanFrom { get; set; }
        public int Seat1 { get; set; }
        public int Seat2 { get; set; }
        public int OnAir { get; set; }
        
        public string Date => DateTime.ToString("MM/dd/yyyy");
        
        public string Time => DateTime.ToString("hh:mm tt");

    }
    public class Datve
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ClientName { get; set; }
        public string Cmnd { get; set; }
        public string Sdt { get; set; }
        public int Price { get; set; }
    }

    public class ProductForHomePageViewModel
    {
        public IEnumerable<ProductCategory> ListProductCategory { get; set; }
        public IEnumerable<SelectListItem> ListPlanFrom { get; set; }
        public int SeletedPlanFromId { get; set; }
        public int SeletedPlanToId { get; set; }
    }
}