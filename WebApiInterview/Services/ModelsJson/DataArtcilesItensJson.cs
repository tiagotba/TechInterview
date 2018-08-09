using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiInterview.Services.ModelsJson
{
    public class DataArtcilesItensJson
    {
        [JsonProperty("id")]
        public string id_article { get; set; }

        [JsonProperty("quantity")]
        public string quant { get; set; }
    }
}
