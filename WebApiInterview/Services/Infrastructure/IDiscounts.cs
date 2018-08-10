using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;
using WebApiInterview.Services.ModelsJson;

namespace WebApiInterview.Services.Infrastructure
{
   public interface IDiscounts
    {
        IEnumerable<Discounts> GetAll();
        void Insert(DataDiscountsJson discounts);
        void Update(DataDiscountsJson discounts);
        DataDiscountsJson Find(long id);
        void Delete(long id);
    }
}
