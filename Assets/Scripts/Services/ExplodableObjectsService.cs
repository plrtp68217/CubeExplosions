using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodableObjectsService : MonoBehaviour
{
    private readonly List<ExplodableObject> _objects = new();


    public List<ExplodableObject> GetNeighbors(ExplodableObject obj)
    {
        return _objects.Where(x => x != obj).ToList();
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
