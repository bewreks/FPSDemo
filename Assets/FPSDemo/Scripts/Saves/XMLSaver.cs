using System.IO;
using System.Xml;

namespace FPSDemo
{
    public class XMLSaver : BaseSaver
    {
        public XMLSaver()
        {
            _extension = "xml";
        }

        protected override void InternalSave(string path, SerializableObject serialized)
        {
            var doc = new XmlDocument();
            XmlNode root = doc.CreateElement(serialized.Name);
            doc.AppendChild(root);

            foreach (var key in serialized.Floats.Keys)
            {
                var element = doc.CreateElement("Element");
                string type;
                string newKey;
                SerializableObject.Split(key, out type, out newKey);
                element.SetAttribute("type", type);
                element.SetAttribute("name", newKey);
                element.SetAttribute("value", serialized.Floats[key].ToString());
                root.AppendChild(element);
            }

            foreach (var key in serialized.Ints.Keys)
            {
                var element = doc.CreateElement("Element");
                string type;
                string newKey;
                SerializableObject.Split(key, out type, out newKey);
                element.SetAttribute("type", type);
                element.SetAttribute("name", newKey);
                element.SetAttribute("value", serialized.Ints[key].ToString());
                root.AppendChild(element);
            }

            doc.Save(path);
        }

        protected override SerializableObject InternalLoad(string path, string name)
        {
            var serializableObject = new SerializableObject(name);

            if (!File.Exists(path))
            {
                return serializableObject;
            }

            using (XmlTextReader reader = new XmlTextReader(path))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement("Element"))
                    {
                        serializableObject.AddProperty(
                            reader.GetAttribute("type"),
                            reader.GetAttribute("name"),
                            reader.GetAttribute("value")
                        );
                    }
                }
            }

            return serializableObject;
        }
    }
}