using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;

namespace WebApiInterview.Services.Infrastructure
{
   public interface IDiscounts
    {
        IEnumerable<Discounts> GetAll();
        void Insert(Discounts discounts);
        void Update(Discounts discounts);
        Discounts Find(long id);
        void Delete(long id);
    }
}
