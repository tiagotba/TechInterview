using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInterview.Context;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;

namespace WebApiInterview.Services.Repository
{
    public class ArticleRepository : IArticles
    {
        private readonly Interview_Context _context;

        public ArticleRepository(Interview_Context context)
        {
            _context = context;
        }

        public void Delete(long id)
        {
            var entity = _context.articles.First(t => t.id == id);
            _context.articles.Remove(entity);
            _context.SaveChanges();
        }

        public Articles Find(long id)
        {
            return _context.articles.FirstOrDefault(t => t.id == id);
        }

        public IEnumerable<Articles> GetAll()
        {
            return _context.articles.ToList();
        }

        public decimal GetDiscount(long id)
        {
          Discounts disc =  _context.discounts.FirstOrDefault(d => d.id_discount == id);
            return disc.value_discount;
        }

        public void Insert(Articles articles)
        {
            _context.articles.Add(articles);
            _context.SaveChanges();
        }

        public void Update(Articles articles)
        {
            _context.articles.Update(articles);
            _context.SaveChanges();
        }
    }
}
