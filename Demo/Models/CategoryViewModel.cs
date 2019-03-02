using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class CategoryViewModel
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public List<string> GetCatList { get; set; }

        public string[] GetList { get; set; }
    }
}