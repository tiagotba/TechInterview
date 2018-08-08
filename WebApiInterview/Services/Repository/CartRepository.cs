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
            var entity = _context.carts.First(t => t.id_cart == id);
            _context.carts.Remove(entity);
            _context.SaveChanges();
        }

        public Carts Find(long id)
        {
            return _context.carts.FirstOrDefault(t => t.id_cart == id);
        }

        public IEnumerable<Carts> GetAll()
        {
            return _context.carts.ToList();
        }

        public void Insert(Carts carts)
        {
            _context.carts.Add(carts);
            _context.SaveChanges();
        }
    }
}
