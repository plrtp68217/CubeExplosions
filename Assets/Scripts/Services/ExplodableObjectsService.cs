using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ExplodableObjectsService: MonoBehaviour
    {
        private readonly List<ExplodableObject> _objects = new();

        public List<ExplodableObject> Objects => _objects;

        public void Register(ExplodableObject obj)
        {
            if (_objects.Contains(obj) == false)
            {
                _objects.Add(obj);
            }
        }

        public void Unregister(ExplodableObject obj)
        {
            _objects.Remove(obj);
        }
    }
}