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

        objectAtPosition.TryGetComponent<ExplodableObject>(out var explodableObject);

        if (explodableObject == null)
        {
            return;
        }

        if (explodableObject.CanSeparating)
        {
            var spawnedObjects = _spawner.SpawnObjects(explodableObject);

            _explodableObjectsService.Register(spawnedObjects);

            _exploder.Explode(spawnedObjects, explodableObject);
        }
        else
        {
            var neighbors = _explodableObjectsService.GetNeighbors(explodableObject);

            _exploder.Explode(neighbors, explodableObject);
        }

        _explodableObjectsService.UnRegister(explodableObject);

        Destroy(explodableObject.gameObject);
    }
}
