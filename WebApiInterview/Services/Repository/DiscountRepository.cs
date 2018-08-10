using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Context;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;
using WebApiInterview.Services.ModelsJson;

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
            var entity = _context.Discounts.First(t => t.id_discount == id);
            _context.Discounts.Remove(entity);
            _context.SaveChanges();
        }

        public DataDiscountsJson Find(long id)
        {
            Discounts ldiscount = _context.Discounts.FirstOrDefault(t => t.id_discount == id);
            DataDiscountsJson discountsJson = new DataDiscountsJson();
            if (ldiscount == null)
            {
                discountsJson = null;
                return discountsJson;
            }
            else
            {
                discountsJson.type_discount = ldiscount.type_discount;
                discountsJson.value_discount =  ldiscount.value_discount.ToString();
                return discountsJson;
            }
            
        }

        public IEnumerable<Discounts> GetAll()
        {
            return _context.Discounts.ToList();
        }

        public void Insert(DataDiscountsJson discounts)
        {
            Discounts ldiscount = new Discounts();
            ldiscount.type_discount = discounts.type_discount;
            ldiscount.value_discount = Convert.ToDecimal( discounts.value_discount);

            _context.Discounts.Add(ldiscount);
            _context.SaveChanges();
        }

        public void Update(Discounts discounts)
        {
            _context.Discounts.Update(discounts);
            _context.SaveChanges();
        }
    }
}
