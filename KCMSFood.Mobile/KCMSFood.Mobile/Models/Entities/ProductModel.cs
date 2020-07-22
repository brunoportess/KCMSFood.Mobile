using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCMSFood.Mobile.Models.Entities
{
    public class ProductModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
