using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiInterview.Context;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WebApiInterview.Services.ModelsJson;
using WebApiInterview.Services.Repository;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiInterview.Controllers
{
    [Route("api/[controller]")]
    public class CartsController : Controller
    {
        private readonly ICarts _carts;
        private readonly IArticles _articles;
        private readonly IDiscounts _discounts;


        public CartsController(ICarts carts, IArticles articles, IDiscounts discounts)
        {
            _carts = carts;
            _articles = articles;
            _discounts = discounts;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public ObjectResult Post([FromBody]List<DataArtcilesItensJson> json)
        {
            DataArtcilesJson resultArticle ;
            DataCartsJsonOutput ret;
            Articles articles;

            Carts cart = new Carts();
            cart.articles = new List<Articles>();

            foreach (var article in json)
            {
                resultArticle = _articles.Find(Convert.ToInt64(article.id_article));

                if ( resultArticle == null )
                {
                    var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new System.Net.Http.StringContent(string.Format("No product with ID = {0}", article.id_article)),
                        ReasonPhrase = "Product Not Found"
                    };
                    throw new Exception(resp.ReasonPhrase);
                }

                if (article.id_discount != null && article.id_discount != "0")
                {
                    var discount = _articles.GetDiscount(Convert.ToInt64(article.id_discount));
                    if (discount > 0)
                    {
                        resultArticle.price =(Convert.ToDecimal(resultArticle.price) - Convert.ToDecimal(discount)).ToString();
                    }
                    else
                    {
                        var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                        {
                            Content = new System.Net.Http.StringContent(string.Format("No discount for Product with ID = {0}", resultArticle.id)),
                            ReasonPhrase = "Discount Not Found"
                        };
                        throw new Exception(resp.ReasonPhrase);
                    }
                }

                articles = new Articles();
                articles.id = Convert.ToInt64( resultArticle.id);
                articles.price = Convert.ToDecimal(resultArticle.price);
                articles.idDiscount = Convert.ToInt32(resultArticle.idDiscount);
                cart.articles.Add(articles);
                cart.quantity_cart = cart.articles.Count;
                cart.total_cart += Convert.ToDecimal(resultArticle.price);
            }
            
            if (cart.total_cart < 1000)
            {
                cart.value_delivery = 800;
            }
            else
                if (cart.total_cart > 1000 && cart.total_cart > 2000)
            {
                cart.value_delivery = 400;
            }
            else
            {
                cart.value_delivery = 0;
            }

            cart.id_cart += 1;
            _carts.Insert(cart);
            ret = new DataCartsJsonOutput();
            ret.id_cart = Convert.ToString(cart.id_cart);
            ret.total_cart = Convert.ToString(cart.total_cart);

            return Ok(ret);
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
        }
    }
}
