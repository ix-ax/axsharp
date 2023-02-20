using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Helpers
{
    public class YamlSerializerHelper
    {
        public YamlSerializerHelper()
        {
          
        }
        //declared schema. which we serialize
        public YamlSchema Schema { get; set; } = new YamlSchema();
        public TocSchema TocSchema { get; set; } = new TocSchema();
        public TocSchemaList TocSchemaList { get; set; } = new TocSchemaList();


        //helpers lists for items
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Item> NamespaceItems { get; set; } = new List<Item>();

    
        //helpers lists for references

        public List<Reference> References { get; set; } = new List<Reference>();  
        public List<Reference> NamespaceReferences { get; set; } = new List<Reference>();  

       
    }
}
