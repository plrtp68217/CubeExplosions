using Assets.Scripts.Types.Interfaces;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour, ISeparable
{
    public int separationChance = 100;

    public Action<Cube> SuccessDestroyed;

    private int _separationChance = 100;

    private readonly int _explosionDelay = 2;
    private readonly float _explosionForce = 500f;
    private readonly float _explosionRadius = 10f;

    public int SeparationChance => _separationChance;

    public void Destroy()
    {
        Disappear();

        if (RandomUtils.IsSuccess(separationChance))
        {
            SuccessDestroyed?.Invoke(this);
            StartCoroutine(ExplodeAndDestroy());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ExplodeAndDestroy()
    {
        yield return new WaitForSeconds(_explosionDelay);

        ApplyExplosion();

        Destroy(gameObject);
    }

    private void ApplyExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            hit.TryGetComponent<Rigidbody>(out var rb);

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private void Disappear()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }
}