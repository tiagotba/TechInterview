using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;

namespace WebApiInterview.Services.Infrastructure
{
  public  interface ICarts
    {
        IEnumerable<Carts> GetAll();
        void Insert(Carts carts);
        Carts Find(long id);
        void Delete(long id);
    }
}
