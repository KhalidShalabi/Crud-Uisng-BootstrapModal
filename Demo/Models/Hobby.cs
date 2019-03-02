using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Hobby
    {
        public int Id { get; set; }


        [ForeignKey("Names"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HobbyiesId { get; set; }

        [ForeignKey("Users"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        public virtual Names Names { get; set; }
        public virtual ApplicationUser Users { get; set; }
        public bool IsChecked { get; set; }
    }

    public class Names
    {
        public int Id { get; set; }
        public string Hobbies { get; set; }

    }
}