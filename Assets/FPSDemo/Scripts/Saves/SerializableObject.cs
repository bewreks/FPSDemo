using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FPSDemo
{
    // Лень сейчас реализовывать все, но в именах нельзя использовать подчеркивания
    public class SerializableObject
    {
        public const string FloatName = "float";
        public const string IntName = "int";
        
        private readonly string _name;
        private readonly Dictionary<string, float> _floats = new Dictionary<string, float>();
        private readonly Dictionary<string, int> _ints = new Dictionary<string, int>();

        public string Name => _name;

        public Dictionary<string, float> Floats => _floats;
        public Dictionary<string, int> Ints => _ints;

        public SerializableObject(string name)
        {
            _name = name;
        }

        public void AddFloat(string name, float value)
        {
            _floats.Add($"{FloatName}_{name}", value);
        }

        public void AddVector3(string name, Vector3 value)
        {
            AddFloat($"{name}_x", value.x);
            AddFloat($"{name}_y", value.y);
            AddFloat($"{name}_z", value.z);
        }

        public void AddQuaternion(string name, Quaternion value)
        {
            AddFloat($"{name}_x", value.x);
            AddFloat($"{name}_y", value.y);
            AddFloat($"{name}_z", value.z);
            AddFloat($"{name}_w", value.w);
        }

        public void AddInt(string name, int value)
        {
            _ints.Add($"{IntName}_{name}", value);
        }

        public float GetFloat(string name)
        {
            float value;
            _floats.TryGetValue($"{FloatName}_{name}", out value);
            return value;
        }

        public Vector3 GetVector3(string name)
        {
            return new Vector3(
                GetFloat($"{name}_x"),
                GetFloat($"{name}_y"),
                GetFloat($"{name}_z")
            );
        }

        public Quaternion GetQuaternion(string name)
        {
            return new Quaternion(
                GetFloat($"{name}_x"),
                GetFloat($"{name}_y"),
                GetFloat($"{name}_z"),
                GetFloat($"{name}_w")
            );
        }

        public int GetInt(string name)
        {
            int value;
            _ints.TryGetValue($"{IntName}_{name}", out value);
            return value;
        }

        public void AddProperty(string name, string property)
        {
            string type;
            Split(name, out type, out name);
            AddProperty(type, name, property);
        }

        public void AddProperty(string type, string name, string property)
        {
            switch (type)
            {
                case FloatName:
                    float floatValue;
                    float.TryParse(property, out floatValue);
                    AddFloat(name, floatValue);
                    break;
                case IntName:
                    int intValue;
                    int.TryParse(property, out intValue);
                    AddInt(name, intValue);
                    break;
            }
        }

        public static void Split(string baseName, out string type, out string name)
        {
            name = Split(baseName, out type);
        }

        public static string Split(string baseName, out string type)
        {
            type = baseName.Split('_')[0];
            return baseName.Replace($"{type}_", "");
        }

        public static string Split(string baseName)
        {
            string type;
            return Split(baseName, out type);
        }

        public void AddSerialable(SerializableObject serialize)
        {
            foreach (var key in serialize.Floats.Keys)
            {
                string type;
                string name;
                Split(key, out type, out name);
                AddFloat($"{serialize.Name}_{name}", serialize.Floats[key]);
            }

            foreach (var key in serialize.Ints.Keys)
            {
                string type;
                string name;
                Split(key, out type, out name);
                AddInt($"{serialize.Name}_{name}", serialize.Ints[key]);
            }
        }

        public SerializableObject GetSerialable(string name)
        {
            var serializableObject = new SerializableObject(name);
            foreach (var key in _floats.Keys)
            {
                if (Regex.IsMatch(key, $"^{FloatName}_{name}_[a-zA-Z_]*$"))
                {
                    var newKey = key.Replace($"{FloatName}_{name}_", "");
                    serializableObject.AddFloat(newKey, _floats[key]);
                }
            }
            
            foreach (var key in _ints.Keys)
            {
                if (Regex.IsMatch(key, $"^{IntName}_{name}_[a-zA-Z_]*$"))
                {
                    var newKey = key.Replace($"{IntName}_{name}_", "");
                    serializableObject.AddInt(newKey, _ints[key]);
                }
            }

            return serializableObject;
        }
    }
}