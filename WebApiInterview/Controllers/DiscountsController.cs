using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiInterview.Models;
using WebApiInterview.Services.Infrastructure;
using WebApiInterview.Services.ModelsJson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiInterview.Controllers
{
    [Route("api/[controller]")]
    public class DiscountsController : Controller
    {
        private readonly IDiscounts _discountRepositorio;

        public DiscountsController(IDiscounts discountsRepositorio)
        {
            _discountRepositorio = discountsRepositorio;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Discounts> Get()
        {
            var discounts = _discountRepositorio.GetAll();
            return discounts;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Discounts Get(int id)
        {
            var discount = _discountRepositorio.Find(id);
            Discounts lDiscount = new Discounts();

            if (discount == null)
            {
                var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new System.Net.Http.StringContent(string.Format("No discount with ID = {0}", id)),
                    ReasonPhrase = "Discount Not Found"
                };
                throw new Exception(resp.ReasonPhrase);
            }

            lDiscount.id_discount = Convert.ToInt64( discount.id_discount);
            lDiscount.type_discount = discount.type_discount;
            lDiscount.value_discount = Convert.ToDecimal(discount.value_discount);

            return lDiscount;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]DataDiscountsJson value)
        {

            if (value != null)
            {
                if (_discountRepositorio.Find(Convert.ToInt64(value.id_discount)) != null)
                    _discountRepositorio.Update(value);
                else
                    _discountRepositorio.Insert(value);
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
            if (_discountRepositorio.Find(id) != null)
            {
                _discountRepositorio.Delete(id);
            }
            else
            {
                var resp = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new System.Net.Http.StringContent(string.Format("No discount with ID = {0}", id)),
                    ReasonPhrase = "Discount Not Found"
                };
                throw new Exception(resp.ReasonPhrase);
            }
        }
    }
}
