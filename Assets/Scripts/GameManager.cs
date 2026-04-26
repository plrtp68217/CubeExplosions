using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private ExplodableObjectsService _explodableObjectsService;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _inputService.Pressed += OnPressed;
    }

    private void OnDisable()
    {
        _inputService.Pressed -= OnPressed;
    }

    private void OnPressed()
    {
        Vector3 pressPosition = _inputService.PressPosition;

        GameObject objectAtPosition = _raycaster.GetObjectAtPosition(pressPosition);

        List<ExplodableObject> objectsToExplode;

        objectAtPosition.TryGetComponent<ExplodableObject>(out var explodableObject);

        if (explodableObject == null)
        {
            return;
        }

        if (explodableObject.CanSeparate)
        {
            objectsToExplode = _spawner.SpawnObjects(explodableObject);

            _explodableObjectsService.Register(objectsToExplode);
        }
        else
        {
            objectsToExplode = _explodableObjectsService.GetNeighbors(explodableObject);
        }

        if (objectsToExplode.Count > 0)
        {
            _exploder.Explode(objectsToExplode, explodableObject);
            _explodableObjectsService.UnRegister(explodableObject);
        }

        Destroy(explodableObject.gameObject);
    }
}
