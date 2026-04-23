using Assets.Scripts.Types.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    private readonly int _minObjectsValue = 2;
    private readonly int _maxObjectsValue = 6;

    private void SpawnObjects(ISeparable separableObject)
    {
        int objectsCount = RandomUtils.GetValue(_minObjectsValue, _maxObjectsValue);

        for (int i = 0; i < objectsCount; i++)
        {
            
        }
    }
}