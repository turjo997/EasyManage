using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyManage.Models
{
    public class ManageProduct
    {
        //public string ItemId { get; set; }
        //public decimal Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
        //public decimal Total { get; set; }

        //public string ImagePath { get; set; }
        //public string ItemName { get; set; }

        public int productid { get; set; }
        public string productname { get; set; }

        public float price { get; set; }
        public int qty { get; set; }
        public float bill { get; set; }


    }
}