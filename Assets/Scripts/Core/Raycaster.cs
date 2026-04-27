using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _maxDistance = Mathf.Infinity;

    public GameObject GetObjectAtPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _targetLayer))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}