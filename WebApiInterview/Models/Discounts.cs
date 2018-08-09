using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiInterview.Models
{
    public class Discounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id_discount { get; set; }

        public string type_discount { get; set; }

        public decimal value_discount { get; set; }

        public List<Articles> articles { get; set; }
    }
}
