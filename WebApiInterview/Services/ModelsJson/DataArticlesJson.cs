using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiInterview.Services.ModelsJson
{
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
}
