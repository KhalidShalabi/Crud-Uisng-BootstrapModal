using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Goods
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This Field Is Valid")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Is Valid")]
        public int Quntity { get; set; }
        [Required(ErrorMessage = "This Field Is Valid")]
        public int TypeId { get; set; }
        public virtual Types Type { get; set; }
    }
}