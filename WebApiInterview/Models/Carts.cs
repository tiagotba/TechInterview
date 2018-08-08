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

        public int id_article { get; set; }

        public int id_discount { get; set; }

        public int quantity_cart { get; set; }
    }
}
