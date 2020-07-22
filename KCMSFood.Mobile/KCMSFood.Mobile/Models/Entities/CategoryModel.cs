using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCMSFood.Mobile.Models.Entities
{
    public class CategoryModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
