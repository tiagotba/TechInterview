using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiInterview.Services.ModelsJson
{
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
}
