using Assets.Scripts.Services;
using Assets.Scripts.Types.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    private void Start()
    {
        var objects = ObjectsService<Cube>.Objects;

        foreach (var obj in objects)
        {
            obj.Separating += SpawnObjects;
        }
    }

    private void SpawnObjects(ISeparable separable)
    {
        separable.Separating -= SpawnObjects;

        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            ISeparable newSeparable = separable.Clone();

            newSeparable.Separating += SpawnObjects;
        }
    }
}