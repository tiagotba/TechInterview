using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiInterview.Services.ModelsJson
{
    public class DataCartsJsonOutput
    {
        [JsonProperty("id")]
        public string id_cart { get; set; }

        [JsonProperty("total")]
        public string total_cart { get; set; }
    }
}
