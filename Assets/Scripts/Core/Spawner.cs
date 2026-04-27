using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ExplodableObject _prefab;

    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    private readonly int _scaleModifier = 2;
    private readonly int _chanceModifier = 2;

    public List<ExplodableObject> SpawnObjects(ExplodableObject parentObject)
    {
        var objects = new List<ExplodableObject>();

        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            ExplodableObject spawnedObject = Instantiate(_prefab, parentObject.transform.position, Quaternion.identity);

            spawnedObject.transform.localScale = parentObject.transform.localScale / _scaleModifier;
            spawnedObject.SetSeparationChance(parentObject.SeparationChance / _chanceModifier);
            spawnedObject.SetRandomColor();

            objects.Add(spawnedObject);
        }

        return objects;
    }

    public void DestroyObject(ExplodableObject explodableObnject)
    {
        Destroy(explodableObnject.gameObject);
    }
}