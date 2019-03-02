using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        [ForeignKey("Country"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}