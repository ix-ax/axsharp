using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Schemas
{
    public class TocSchema
    {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public Item[] Items { get; set; }
        
        public partial class Item
        {
            [JsonProperty("uid")]
            public string Uid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
            public Item[] Items { get; set; }
        }
    }

    public class TocSchemaList
    {
        public List<ItemList> Items { get; set; } = new List<ItemList>();

        public partial class ItemList
        {
            public string Uid { get; set; }

            public string Name { get; set; }

            public List<ItemList> Items { get; set; } = new List<ItemList>();
        }
    }
}

