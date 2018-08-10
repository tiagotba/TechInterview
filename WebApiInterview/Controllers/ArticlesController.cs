using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;
using WebApiInterview.Services.ModelsJson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiInterview.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IArticles _articleRepositorio;

        public ArticlesController(IArticles articleRepositorio)
        {
            _articleRepositorio = articleRepositorio;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Articles> Get()
        {
            var articles = _articleRepositorio.GetAll();
            return articles;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Articles Get(int id)
        {
            var article = _articleRepositorio.Find(id);
            Articles lArtcile = new Articles();

            if (article == null)
            {
                var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new System.Net.Http.StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product Not Found"
                };
                throw new Exception(resp.ReasonPhrase);
            }

            lArtcile.id = Convert.ToInt64( article.id);
            lArtcile.name = article.name;
            lArtcile.price = Convert.ToDecimal( article.price);


            return lArtcile;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]DataArtcilesJson pResultArticle)
        {
            if (pResultArticle != null)
            {

                if ( _articleRepositorio.Find(Convert.ToInt64(pResultArticle.id)) != null )
                    _articleRepositorio.Update(pResultArticle);
                else
                    _articleRepositorio.Insert(pResultArticle);
            }


        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_articleRepositorio.Find(Convert.ToInt64(id)) != null)
            {
                _articleRepositorio.Delete(id);
            }
            else
            {
                var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new System.Net.Http.StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product Not Found"
                };
                throw new Exception(resp.ReasonPhrase);
            }

        }
    }


}
