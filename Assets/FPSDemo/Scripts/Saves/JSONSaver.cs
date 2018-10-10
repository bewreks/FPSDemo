using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FPSDemo
{
    public class JSONSaver : BaseSaver
    {
        public struct JSONObject
        {
            public string type;
            public string name;
            public string value;
        }
        
        public JSONSaver()
        {
            _extension = "json";
        }

        protected override void InternalSave(string path, SerializableObject serialized)
        {
            var objects = new List<JSONObject>();
            objects.AddRange(from key in serialized.Floats.Keys
                let strType = SerializableObject.FloatName
                let strName = SerializableObject.Split(key)
                select new JSONObject
                {
                    type = strType,
                    name = strName,
                    value = serialized.Floats[key].ToString()
                });
            objects.AddRange(from key in serialized.Ints.Keys
                let strType = SerializableObject.IntName
                let strName = SerializableObject.Split(key)
                select new JSONObject
                {
                    type = strType,
                    name = strName,
                    value = serialized.Ints[key].ToString()
                });

            var serializeObject = JsonConvert.SerializeObject(objects);
            File.WriteAllText(path, serializeObject);
        }

        protected override SerializableObject InternalLoad(string path, string name)
        {
            var serializableObject = new SerializableObject(name);
            if (!File.Exists(path))
            {
                return serializableObject;
            }

            var readAllText = File.ReadAllText(path);
            var objects = JsonConvert.DeserializeObject(readAllText, typeof(List<JSONObject>)) as List<JSONObject>;
            objects.ForEach(o => serializableObject.AddProperty(o.type, o.name, o.value));
            return serializableObject;
        }
    }
}