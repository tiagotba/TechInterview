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
    public class ArticleRepository : IArticles
    {
        private readonly Interview_Context _context;

        public ArticleRepository(Interview_Context context)
        {
            _context = context;
        }

        public void Delete(long id)
        {
            var entity = _context.Articles.First(t => t.id == id);
            _context.Articles.Remove(entity);
            _context.SaveChanges();
        }

        public DataArtcilesJson Find(long id)
        {
            Articles lArticle = _context.Articles.FirstOrDefault(t => t.id == id);
            DataArtcilesJson dataArtciles = new DataArtcilesJson();
            if (lArticle == null)
            {
                dataArtciles = null;
                return dataArtciles;
            }
            else
            {
                dataArtciles.id = lArticle.id.ToString();
                dataArtciles.name = lArticle.name;
                dataArtciles.price = lArticle.price.ToString();
                dataArtciles.idDiscount = lArticle.idDiscount.ToString();
                return dataArtciles;
            }
          
        }

        public IEnumerable<Articles> GetAll()
        {
            return _context.Articles.ToList();
        }

        public decimal GetDiscount(long id)
        {
          Discounts disc =  _context.Discounts.FirstOrDefault(d => d.id_discount == id);
            return disc.value_discount;
        }

        public void Insert(DataArtcilesJson pArticles)
        {
            Articles lArticle = new Articles();
            lArticle.id = Convert.ToInt64(pArticles.id);
            lArticle.name = pArticles.name;
            lArticle.price = Convert.ToDecimal(pArticles.price);
            lArticle.idDiscount = Convert.ToInt32( pArticles.idDiscount);

            _context.Articles.Add(lArticle);
            _context.SaveChanges();
        }

        public void Update(DataArtcilesJson pArticles)
        {
            DataArtcilesJson lDataArticle = Find(Convert.ToInt64(pArticles.id));
            Articles lArticle = new Articles();
            lArticle.id =  Convert.ToInt64( lDataArticle.id);
            lArticle.name = pArticles.name;
            lArticle.price = Convert.ToDecimal(pArticles.price);
            lArticle.idDiscount = 0;

            _context.Articles.Update(lArticle);
            _context.SaveChanges();
        }
    }
}
