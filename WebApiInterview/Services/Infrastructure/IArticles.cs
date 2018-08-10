using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;
using WebApiInterview.Services.ModelsJson;

namespace WebApiInterview.Services.Infrastructure
{
  public  interface IArticles
    {
        IEnumerable<Articles> GetAll();
        void Insert(DataArtcilesJson articles);
        void Update(DataArtcilesJson articles);
        DataArtcilesJson Find(long id);
        Decimal GetDiscount(long id);
        void Delete(long id);
    }
}
