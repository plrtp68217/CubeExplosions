using Assets.Scripts.Services;
using Assets.Scripts.Types.Interfaces;
using Assets.Scripts.Utils;
using System;
using UnityEngine;

public class Cube : MonoBehaviour, ISeparable
{
    private int _separationChance = 100;

    private readonly float _explosionForce = 500f;
    private readonly float _explosionRadius = 10f;

    public event Action<ISeparable> Separating;

    public int SeparationChance
    {
        get => _separationChance;
        set => _separationChance = value;
    }

    private void OnEnable()
    {
        ObjectsService<ISeparable>.Register(this);
    }

    private void OnDisable()
    {
        ObjectsService<ISeparable>.Unregister(this);
    }

    public void TrySeparate()
    {
        if (RandomUtils.IsSuccess(SeparationChance))
        {
            Separating?.Invoke(this);
        }

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

    public ISeparable Clone()
    {
        var clone = Instantiate(this, transform.position, Quaternion.identity);

        clone.transform.localScale = transform.localScale / 2;

        clone.SeparationChance = _separationChance / 2;

        Color randomColor = new(
            UnityEngine.Random.value,
            UnityEngine.Random.value,
            UnityEngine.Random.value
        );

        Renderer renderer = clone.GetComponent<Renderer>();

        renderer.material.color = randomColor;

        return clone;
    }
}