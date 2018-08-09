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
        public DataCartsJsonOutput Post([FromBody]List<DataArtcilesItensJson> jArticles,  [FromBody]List<DataDiscountsJson> jDiscounts)
        {
            Articles resultArticle ;
            DataCartsJsonOutput ret;
            //Discounts resultDiscount = new Discounts();

            Carts cart = new Carts();
            cart.articles = new List<Articles>();

            foreach (var article in jArticles)
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
                else
                {
                  
                }

                //foreach (var discount in jDiscounts)
                //{
                //    resultDiscount = JsonConvert.DeserializeObject<Discounts>(discount);
                //    if(resultArticle.idDiscount == resultDiscount.id_discount)
                //    {
                //        resultArticle.price = resultArticle.price - resultDiscount.value_discount;
                //    }
                //}
                
                cart.articles.Add(resultArticle);
                cart.quantity_cart = cart.articles.Count;
                cart.total_cart += resultArticle.price;
            }

            cart.id_cart += 1;
            _carts.Insert(cart);
            ret = new DataCartsJsonOutput();
            ret.id_cart = Convert.ToString(cart.id_cart);
            ret.total_cart = Convert.ToString(cart.total_cart);

            return ret;
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
