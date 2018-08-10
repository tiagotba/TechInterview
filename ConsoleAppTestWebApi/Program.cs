using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleAppTestWebApi
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string typeDiscount = "";
            string valueDiscount = "";
            DataArtcilesJson gDataArtcilesJson = null;
            DataCartsJsonOutput gDataCartsJsonOutput = null;
            DataArtcilesItensJson gDataArtcilesItensJson = null;

            Console.WriteLine("Hello! Welcome Test Web Api Interview. Click enter to start.");
            Console.ReadKey();

            Console.WriteLine("Do you want to register a product?. (Y or N) ");
            string confirmRegister = Console.ReadLine();
            if (confirmRegister == "Y")
            {
                Console.WriteLine("Enter the code of the product ");
                string codP = Console.ReadLine();
                Console.WriteLine("Enter the name of the product ");
                string nameP = Console.ReadLine();
                Console.WriteLine("Enter the value of the product ");
                string valueP = Console.ReadLine();
                Console.WriteLine("Will the product have any discounts? (Y or N) ");
                string confirmDiscount = Console.ReadLine();
                if (confirmDiscount == "Y")
                {
                    Console.WriteLine(" What kind of discount? A for amount and P for percentage ");
                     typeDiscount = Console.ReadLine();
                    Console.WriteLine(" Enter the discount amount");
                    valueDiscount = Console.ReadLine();
                }
                else
                {
                    typeDiscount = "";
                    valueDiscount = "0";
                }

                Console.WriteLine(" Do you want to save the product? (Y or N) ");
                string confirmSave = Console.ReadLine();

                if (confirmSave == "Y")
                {
                    Console.WriteLine(AddProduct(codP,nameP, valueP, confirmDiscount != "" ? true : false, valueDiscount));
                    Console.ReadKey();
                }
            }
            else
            {
                List<DataArtcilesItensJson> ldataArtc = new List<DataArtcilesItensJson>();
                Console.WriteLine("Do you want to make a purchase?. (Y or N) ");
                string confirmPurchase = Console.ReadLine();
                Console.WriteLine("Enter the code of the product ");
                string codP = Console.ReadLine();
                gDataArtcilesJson = SearchProduct(codP);
                Console.WriteLine("Enter the total quantity? (Only numbers)");
                string qProd = Console.ReadLine();
                gDataArtcilesItensJson = new DataArtcilesItensJson();
                gDataArtcilesItensJson.id_article = gDataArtcilesJson.id;
                gDataArtcilesItensJson.id_discount = gDataArtcilesJson.idDiscount;
                gDataArtcilesItensJson.quant = qProd;
                ldataArtc.Add(gDataArtcilesItensJson);
                gDataCartsJsonOutput = AddCart(ldataArtc);
                Console.WriteLine(gDataCartsJsonOutput);
            }

             string AddProduct(string pCodigo,string pName, string pValue, bool pDiscount, string pVDisconunt)
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
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    return responseString;
                }
                else
                {
                    return response.ReasonPhrase + " " + response.RequestMessage;
                }
            }

            DataArtcilesJson SearchProduct(string pCodigo)
            {
                DataArtcilesJson pResultArticle = new DataArtcilesJson();

                pResultArticle.id = pCodigo;

                var client = new HttpClient()
                {
                    BaseAddress = new Uri("http://localhost:52046/api/articles/"+ pResultArticle.id)
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

            DataCartsJsonOutput AddCart(List<DataArtcilesItensJson> pDataArticleItens)
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

    // Json object that sends product discounts
    public class DataDiscountsJson
    {
        [JsonProperty("id")]
        public string id_article { get; set; }

        [JsonProperty("type")]
        public string type_discount { get; set; }

        [JsonProperty("value")]
        public string value_discount { get; set; }
    }
}
