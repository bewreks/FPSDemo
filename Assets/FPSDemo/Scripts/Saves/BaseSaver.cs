using System;
using System.Collections.Generic;
using System.IO;

namespace FPSDemo
{
    public abstract class BaseSaver
    {
        protected string _extension;
        
        protected Dictionary<string, SerializableObject> _savables = new Dictionary<string, SerializableObject>();
        protected Dictionary<string, ISerializable> _loadables = new Dictionary<string, ISerializable>();
        
        public void AddSavable(ISerializable savable)
        {
            _savables.Add(savable.SerializedName, savable.Serialize());
        }
        
        public void AddLoadable(ISerializable savable)
        {
            _loadables.Add(savable.SerializedName, savable);
        }

        public void Save(string path)
        {
            foreach (var serialized in _savables.Values)
            {
                InternalSave(Path.Combine(path, $"{serialized.Name}.{_extension}"), serialized);
            }
        }

        public void Load(string path)
        {
            foreach (var name in _loadables.Keys)
            {
                var serialized = InternalLoad(Path.Combine(path, $"{name}.{_extension}"), name);
                _loadables[name].Unserialize(serialized);
            }
        }

        protected abstract void InternalSave(string path, SerializableObject serialized);
        protected abstract SerializableObject InternalLoad(string path, string name);
    }
}