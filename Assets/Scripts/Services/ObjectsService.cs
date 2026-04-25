using Assets.Scripts.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    public static class ObjectsService<T>
    {
        private static readonly List<T> _objects = new();
        public static List<T> Objects => _objects;

        public static void Register(T obj)
        {
            if (!_objects.Contains(obj))
                _objects.Add(obj);
        }

        public static void Unregister(T obj)
        {
            _objects.Remove(obj);
        }
    }
}
