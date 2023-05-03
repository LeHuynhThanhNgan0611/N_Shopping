using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWeb2.Models
{
    public class CartItem
    {
        DBSportStore1Entities db = new DBSportStore1Entities();
        public int ProductID { get; set; }
        public string NamePro { get; set; }
        public string ImagePro { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public string AddressDeliverry { get; set; } // Thêm thuộc tính địa chỉ giao hàng
        //Tính FinalPrice = Price * Number
        public decimal FinalPrice()
        {
            return Number * Price;
        }
        public CartItem(int ProductID)
        {
            this.ProductID = ProductID;

            var productDB = db.Products.Single(s => s.ProductID == this.ProductID);
            this.NamePro = productDB.NamePro;
            this.ImagePro = productDB.ImagePro;
            this.Price = (decimal)productDB.Price;
            this.Number = 1;
            this.AddressDeliverry = null; // Khởi tạo giá trị ban đầu của địa chỉ giao hàng là null
        }

    }
}