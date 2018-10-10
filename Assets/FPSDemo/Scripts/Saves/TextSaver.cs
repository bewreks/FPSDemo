using System.IO;

namespace FPSDemo
{
    public class TextSaver : BaseSaver
    {
        public TextSaver()
        {
            _extension = "sav";
        }

        protected override void InternalSave(string filePath, SerializableObject serialized)
        {
            using (var sw = new StreamWriter(filePath, false))
            {
                foreach (var key in serialized.Floats.Keys)
                {
                    sw.WriteLine(key);
                    sw.WriteLine(serialized.Floats[key]);
                }

                foreach (var key in serialized.Ints.Keys)
                {
                    sw.WriteLine(key);
                    sw.WriteLine(serialized.Ints[key]);
                }
            }
        }

        protected override SerializableObject InternalLoad(string path, string name)
        {
            var serializableObject = new SerializableObject(name);

            if (!File.Exists(path))
            {
                return serializableObject;
            }

            using (var sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var propertyName = sr.ReadLine();
                    var property = sr.ReadLine();
                    serializableObject.AddProperty(propertyName, property);
                }
            }

            return serializableObject;
        }
    }
}