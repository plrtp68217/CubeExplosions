using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodableObjectsService : MonoBehaviour
{
    [SerializeField] private List<ExplodableObject> _objects = new();

    public List<ExplodableObject> GetNeighbors(ExplodableObject obj)
    {
        return _objects.Where(neighbor => neighbor != obj).ToList();
    }

    public void Register(List<ExplodableObject> objects)
    {
        foreach (var obj in objects)
        {
            Register(obj);
        }
    }

    public void Register(ExplodableObject obj)
    {
        if (_objects.Contains(obj) == false)
        {
            _objects.Add(obj);
        }
    }

    public void UnRegister(ExplodableObject obj)
    {
        _objects.Remove(obj);
    }
}