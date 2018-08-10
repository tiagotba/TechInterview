using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiInterview.Services.ModelsJson
{
    // classe de modelo de entrada para Json , foi alterada por não haver necessidade do valor do frete ser passado como parametro no Json
    public class DataArtcilesItensJson
    {
        [JsonProperty("id")]
        public string id_article { get; set; }

        [JsonProperty("quantity")]
        public string quant { get; set; }

        [JsonProperty("idDiscount")]
        public string id_discount { get; set; }
    }
}
