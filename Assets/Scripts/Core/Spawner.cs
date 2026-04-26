using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    public List<ExplodableObject> SpawnObjects(ExplodableObject parentObject)
    {
        var objects = new List<ExplodableObject>();

        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            var spawnedObject = parentObject.Clone();

            objects.Add(spawnedObject);
        }

        return objects;
    }
}