using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 10f;
    
    public void ExplodeOverlap(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius);
        
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out ExplodableObject explodable))
            {
                explodable.ApplyExplosion(position, _explosionForce, _explosionRadius);
            }
        }
    }
    
    public void ExplodeList(List<ExplodableObject> explodableObjects, Vector3 position)
    {
        foreach (ExplodableObject explodable in explodableObjects)
        {
            explodable.ApplyExplosion(position, _explosionForce, _explosionRadius);
        }
    }
}