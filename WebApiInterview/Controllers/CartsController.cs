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
        public void Post([FromBody]List<String> jArticles, [FromBody]JObject jCarts, [FromBody]List<JObject> jDelivery_Fees, [FromBody]List<String> jDiscounts)
        {
            Articles resultArticle ;
            Discounts resultDiscount = new Discounts();
            Carts cart = new Carts();
            cart.articles = new List<Articles>();

            foreach (var article in jArticles)
            {

                resultArticle = JsonConvert.DeserializeObject<Articles>(article);
                if ( _articles.Find(resultArticle.id) == null )
                {
                    _articles.Insert(resultArticle);
                }
 
                foreach (var discount in jDiscounts)
                {
                    resultDiscount = JsonConvert.DeserializeObject<Discounts>(discount);
                    if(resultArticle.idDiscount == resultDiscount.id_discount)
                    {
                        resultArticle.price = resultArticle.price - resultDiscount.value_discount;
                    }
                }

               
                cart.id_cart += 1;
                cart.articles.Add(resultArticle);
                _carts.Insert(cart);

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
        }
    }
}
