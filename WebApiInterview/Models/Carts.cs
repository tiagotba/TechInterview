using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiInterview.Models
{
    public class Carts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id_cart { get; set; }

        //public long id_article { get; set; }

        //public long id_discount { get; set; }

        public List<Articles> articles;

        public decimal value_delivery { get; set; }

        public int quantity_cart { get; set; }

        public decimal total_cart { get; set; }
    }
}
