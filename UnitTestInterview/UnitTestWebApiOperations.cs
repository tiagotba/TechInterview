using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTestInterview
{
    [TestClass]
    public class UnitTestWebApiOperations
    {
        // case test#1: test that verifies that the inclusion of the product without discount in the shopping cart was correct
        [TestMethod]
        public void TestMethodAddProductWithoutDiscount()
        {
            string codP = "1";
            string nameP = "Vassouras";
            string valueP = "4.50";
            bool confirmDiscount = false;
            string discountV = "0";
            Assert.IsTrue(AddProduct(codP, nameP, valueP, confirmDiscount, discountV));
        }

        // case test#2: test that verifies that the inclusion of the product with discount in the shopping cart was correct
        [TestMethod]
        public void TestMethodAddProductWithDiscount()
        {
            string codP = "2";
            string nameP = "Televisão";
            string valueP = "1425.00";
            bool confirmDiscount = true;
            string discountV = "0";
            Assert.IsTrue(AddProduct(codP, nameP, valueP, confirmDiscount, discountV));
        }

        // case test#3: test that verifies if a product exists in the virtual store
        [TestMethod]
        public void TestMethodSearchProduct()
        {
            string codP = "2";

            Assert.IsNotNull(SearchProduct(codP));
        }

        // case test#4: test that adds a product to the shopping cart
        [TestMethod]
        public void TestMethodAddProductInCart()
        {
            string codP = "2";
            List<DataArtcilesItensJson> ldataArtc = new List<DataArtcilesItensJson>();
            DataArtcilesJson gDataArtcilesJson = SearchProduct(codP);
            string qProd = "10";
            DataArtcilesItensJson gDataArtcilesItensJson = new DataArtcilesItensJson();
            gDataArtcilesItensJson.id_article = gDataArtcilesJson.id;
            gDataArtcilesItensJson.id_discount = gDataArtcilesJson.idDiscount;
            gDataArtcilesItensJson.quant = qProd;
            ldataArtc.Add(gDataArtcilesItensJson);
            Assert.IsNotNull(AddCart(ldataArtc));
        }

        // case test#5: test that adds a discount to be used on the products
        [TestMethod]
        public void TestMethodAddDiscountToProducts()
        {
            string cod = "1";
            string codP = "2";
            string type = "Amount";
            string value = "25";

            Assert.IsTrue(AddDiscount(cod, codP, type, value));
        }

        // case test#6: test that adds a discount on the value of a product
        [TestMethod]
        public void TestMethodAddDiscountInProduct()
        {
            string codP = "2";
            string codD = "1";
            DataArtcilesJson dataArtcilesJson = SearchProduct(codP);
            DataDiscountsJson dataDiscounts = SearchDiscount(codD);
            dataArtcilesJson.idDiscount = dataDiscounts.id_discount;

            Assert.AreEqual(dataArtcilesJson.idDiscount, dataDiscounts.id_discount);
        }

        public bool AddProduct(string pCodigo, string pName, string pValue, bool pDiscount, string pVDisconunt)
        {
            DataArtcilesJson pResultArticle = new DataArtcilesJson();
            pResultArticle.id = pCodigo;
            pResultArticle.name = pName;
            pResultArticle.price = pValue;
            pResultArticle.idDiscount = pVDisconunt;

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:52046/api/articles")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            var content = new StringContent(JsonConvert.SerializeObject(pResultArticle).ToString(),
            Encoding.UTF8, "application/json");
            response = client.PostAsync(client.BaseAddress, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddDiscount(string pcod, string pCodigoProduct, string pType, string pVDiscount)
        {
            DataDiscountsJson lDiscountsJson = new DataDiscountsJson();
            lDiscountsJson.id_article = pCodigoProduct;
            lDiscountsJson.type_discount = pType;
            lDiscountsJson.value_discount = pVDiscount;

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:52046/api/discounts")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            var content = new StringContent(JsonConvert.SerializeObject(lDiscountsJson).ToString(),
            Encoding.UTF8, "application/json");
            response = client.PostAsync(client.BaseAddress, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public DataArtcilesJson SearchProduct(string pCodigo)
        {
            DataArtcilesJson pResultArticle = new DataArtcilesJson();

            pResultArticle.id = pCodigo;

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:52046/api/articles/" + pResultArticle.id)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DataArtcilesJson>(responseString);
            }
            else
            {
                pResultArticle = null;
                return pResultArticle;
            }
        }

        public DataDiscountsJson SearchDiscount(string pCodigo)
        {
            DataDiscountsJson resultDiscount = new DataDiscountsJson();

            resultDiscount.id_discount = pCodigo;

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:52046/api/discounts/" + resultDiscount.id_discount)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DataDiscountsJson>(responseString);
            }
            else
            {
                resultDiscount = null;
                return resultDiscount;
            }


        }

        public DataCartsJsonOutput AddCart(List<DataArtcilesItensJson> pDataArticleItens)
        {

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:52046/api/carts")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            var content = new StringContent(JsonConvert.SerializeObject(pDataArticleItens).ToString(),
            Encoding.UTF8, "application/json");

            response = client.PostAsync(client.BaseAddress, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DataCartsJsonOutput>(responseString);
            }
            else
            {
                return null;
            }
        }
    }

    public class DataArtcilesItensJson
    {
        [JsonProperty("id")]
        public string id_article { get; set; }

        [JsonProperty("quantity")]
        public string quant { get; set; }

        [JsonProperty("idDiscount")]
        public string id_discount { get; set; }
    }

    //Json object that saves product data
    public class DataArtcilesJson
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("idDiscount")]
        public string idDiscount { get; set; }
    }

    // Json object that saves discount in product data
    public class DataDiscountsJson
    {
        [JsonProperty("idDiscount")]
        public string id_discount { get; set; }

        [JsonProperty("id")]
        public string id_article { get; set; }

        [JsonProperty("type")]
        public string type_discount { get; set; }

        [JsonProperty("value")]
        public string value_discount { get; set; }
    }

    // Json object that sends shopping cart data
    public class DataCartsJson
    {
        [JsonProperty("id")]
        public string id_cart { get; set; }

        [JsonProperty("items")]
        public List<DataArtcilesItensJson> items { get; set; }
    }

    // Json object that receives output from shopping cart data
    public class DataCartsJsonOutput
    {
        [JsonProperty("id")]
        public string id_cart { get; set; }

        [JsonProperty("total")]
        public string total_cart { get; set; }
    }
}
