using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiInterview.Services.ModelsJson
{
    public class DataCartsJson
    {
        [JsonProperty("id")]
        public string id_cart { get; set; }

        [JsonProperty("items")]
        public List<DataArtcilesItensJson> items { get; set; }
    }
}
