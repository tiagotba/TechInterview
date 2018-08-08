using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Context;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;

namespace WebApiInterview.Services.Repository
{
    public class DiscountRepository : IDiscounts
    {
        private readonly Interview_Context _context;

        public DiscountRepository(Interview_Context context)
        {
            _context = context;
        }

        public void Delete(long id)
        {
            var entity = _context.discounts.First(t => t.id_discount == id);
            _context.discounts.Remove(entity);
            _context.SaveChanges();
        }

        public Discounts Find(long id)
        {
            return _context.discounts.FirstOrDefault(t => t.id_discount == id);
        }

        public IEnumerable<Discounts> GetAll()
        {
            return _context.discounts.ToList();
        }

        public void Insert(Discounts discounts)
        {
            _context.discounts.Add(discounts);
            _context.SaveChanges();
        }

        public void Update(Discounts discounts)
        {
            _context.discounts.Update(discounts);
            _context.SaveChanges();
        }
    }
}
