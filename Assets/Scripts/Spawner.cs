using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Exploader _exploader;
    [SerializeField] private ExplodableObjectsService _explodableObjectsService;

    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    private void OnEnable()
    {
        _raycaster.Exploading += OnExploading;
    }


    private void OnDisable()
    {
        _raycaster.Exploading -= OnExploading;
    }

    private void OnExploading(ExplodableObject obj)
    {
        if (obj.CanSeparating)
        {
            var spawnedObjects = SpawnObjects(obj);
            _exploader.Explode(spawnedObjects, obj);
        }
        else
        {
            var neighbors = _explodableObjectsService.GetNeighbors(obj);
            _exploader.Explode(neighbors, obj);
        }

        Destroy(obj.gameObject);
    }

    private List<ExplodableObject> SpawnObjects(ExplodableObject parentSeparable)
    {
        List<ExplodableObject> objects = new();

        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            ExplodableObject newSeparable = parentSeparable.Clone();
            objects.Add(newSeparable);
        }

        return objects;
    }
}