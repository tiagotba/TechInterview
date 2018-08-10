using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Context;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;

namespace WebApiInterview.Services.Repository
{
    public class CartRepository : ICarts
    {
        private readonly Interview_Context _context;

        public CartRepository(Interview_Context context)
        {
            _context = context;
        }

        public void Delete(long id)
        {
            var entity = _context.Carts.First(t => t.id_cart == id);
            _context.Carts.Remove(entity);
            _context.SaveChanges();
        }

        public Carts Find(long id)
        {
            return _context.Carts.FirstOrDefault(t => t.id_cart == id);
        }

        public IEnumerable<Carts> GetAll()
        {
            return _context.Carts.ToList();
        }

        public void Insert(Carts carts)
        {
            _context.Carts.Add(carts);
            _context.SaveChanges();
        }
    }
}
