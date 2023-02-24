using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Models
{
    public class Comments
    {
        public string summary { get; set; }
        public Dictionary<string, string> param { get; set; } = new();
        public string example { get; set; }
        public string returns { get; set; }
        public string remarks { get; set; }
    }
}
