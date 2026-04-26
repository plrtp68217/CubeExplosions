using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    private readonly float _explosionForce = 100f;
    private readonly float _explosionRadius = 10f;

    public void Explode(List<ExplodableObject> objects, ExplodableObject exploadingObject)
    {
        foreach (ExplodableObject obj in objects)
        {
            obj.TryGetComponent<Rigidbody>(out var rb);

            if (rb != null)
            {
                rb.AddExplosionForce(
                    _explosionForce * exploadingObject.ForceModifier,
                    exploadingObject.transform.position, 
                    _explosionRadius
                );
            }
        }
    }
}