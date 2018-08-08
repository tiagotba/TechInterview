using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Models;

namespace WebApiInterview.Services.Infrastructure
{
  public  interface IArticles
    {
        IEnumerable<Articles> GetAll();
        void Insert(Articles articles);
        void Update(Articles articles);
        Articles Find(long id);
        void Delete(long id);
    }
}
