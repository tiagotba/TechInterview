using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;

namespace WebApiInterview.Context
{
    public class Interview_Context : DbContext
    {
        public Interview_Context(DbContextOptions<Interview_Context> options)
              : base(options)
        { }


        public DbSet<Articles> Articles { get; set; }

        public DbSet<Discounts> Discounts { get; set; }

        public DbSet<Carts> Carts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }
}
