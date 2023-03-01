using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources.NetStandard;

namespace Ix.ixr_doc
{
    /// <summary>
    /// Manager for creating and generating resx file
    /// </summary>
    public static class ResxManager
    {
        /// <summary>
        /// Add resources from dictionary to resx file
        /// </summary>
        /// <param name="path">Path to resx file</param>
        /// <param name="dictionary">Dictionary with resources</param>
        public static void AddResourcesFromDictionary(string path, Dictionary<string, StringValueWrapper> dictionary)
        {
            CreateFileIfNotExist(path);

            using (ResXResourceWriter resx = new ResXResourceWriter(path))
            {
                foreach (KeyValuePair<string, StringValueWrapper> kvp in dictionary)
                {
                    resx.AddResource(new ResXDataNode(kvp.Key, kvp.Value.RawValue) { Comment = $"{kvp.Value.FileName},{kvp.Value.Line}" });
                }
            }
        }

        //Create new file, if file doesnt exist
        private static void CreateFileIfNotExist(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
    }
}
