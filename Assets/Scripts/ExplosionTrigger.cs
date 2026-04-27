using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
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

    private void OnPressed(Vector3 position)
    {
        GameObject objectAtPosition = _raycaster.GetObjectAtPosition(position);

        if (objectAtPosition == null)
        {
            return;
        }

        if (objectAtPosition.TryGetComponent(out ExplodableObject explodableObject) == false)
        {
            return;
        }

        if (explodableObject.CanSeparate)
        {
            List<ExplodableObject> objectsToExplode = _spawner.SpawnObjects(explodableObject);

            _exploder.ExplodeList(objectsToExplode, explodableObject.transform.position);
        }
        else
        {
            _exploder.ExplodeOverlap(explodableObject.transform.position);
        }

        _spawner.DestroyObject(explodableObject);
    }
}
