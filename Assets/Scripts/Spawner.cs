using Assets.Scripts.Services;
using Assets.Scripts.Utils;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ExplodableObjectsService _explodableObjectsService;

    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    private void Start()
    {
        var objects = _explodableObjectsService.Objects;

        foreach (var obj in objects)
        {
            obj.Separating += SpawnObjects;
        }
    }

    private void SpawnObjects(ExplodableObject parentSeparable)
    {
        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            ExplodableObject newSeparable = parentSeparable.Clone();

            newSeparable.SelfExplode();
            newSeparable.Separating += SpawnObjects;
        }

        parentSeparable.Separating -= SpawnObjects;
    }
}