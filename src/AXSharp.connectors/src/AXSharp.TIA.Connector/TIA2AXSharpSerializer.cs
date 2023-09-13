﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.TIA2AXSharp
{
    public static class TIA2AXSharpSerializer
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        { 
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };
        private static JsonSerializer _serializer = JsonSerializer.Create(_settings);

        public static TIARootObject? Deserialize(string path)
        {

            if (!File.Exists(path)) return null;

            using (StreamReader file = File.OpenText(path))
            {
                return (TIARootObject?)_serializer.Deserialize(file, typeof(TIARootObject));
            }
        }


        public static void Serialize(TIARootObject adapter, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, adapter);
            }
        }
    }
}
