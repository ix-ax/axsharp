using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace Ix.ixc_doc
{
    internal class YamlSerializer
    {
        public string TocToYaml(TocSchema schema)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).Build();
            stringBuilder.AppendLine(serializer.Serialize(schema));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@$"D:\Inxton\ix-ax\ix\src\ix.compiler\src\Ix.ixc-doc\output\ax\toc.yml"))
            {

                file.WriteLine("### YamlMime:TableOfContent");
                file.WriteLine(stringBuilder.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("### YamlMime:TableOfContent");
            Console.WriteLine(stringBuilder);


            return stringBuilder.ToString();
        }

        public string SchemaToYaml(YamlSchema model, string fileName)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).Build();
            stringBuilder.AppendLine(serializer.Serialize(model));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@$"D:\Inxton\ix-ax\ix\src\ix.compiler\src\Ix.ixc-doc\output\ax\{fileName}.yml"))
            {

                file.WriteLine("## YamlMime:ManagedReference");
                file.WriteLine(stringBuilder.ToString()); 
            }
            Console.WriteLine("");
            Console.WriteLine("## YamlMime:ManagedReference");
            Console.WriteLine(stringBuilder);

            
            return stringBuilder.ToString();
        }

       
    }
}
