using Assets.Scripts.Services;
using Assets.Scripts.Utils;
using System;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    [SerializeField] private ExplodableObjectsService _explodableObjectsService;

    private int _separationChance = 100;

    private readonly int _minChanceValue = 0;
    private readonly int _maxChanceValue = 100;

    private readonly float _explosionForce = 100f;
    private readonly float _explosionRadius = 10f;

    public event Action<ExplodableObject> Separating;

    public float ForceModifier => 1 / transform.localScale.x;

    private void OnEnable()
    {
        _explodableObjectsService.Register(this);
    }

    private void OnDisable()
    {
        _explodableObjectsService.Unregister(this);
    }

    public void TrySeparate()
    {
        if (RandomUtils.IsSuccess(_separationChance))
        {
            Separating?.Invoke(this);
        }
        else
        {
            GlobalExplode();
        }

        Destroy(gameObject);
    }

    public void SelfExplode()
    {
        TryGetComponent<Rigidbody>(out var rb);

        if (rb != null)
        {
            rb.AddExplosionForce(_explosionForce * ForceModifier, transform.position, _explosionRadius);
        }
    }

    private void GlobalExplode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            hit.TryGetComponent<Rigidbody>(out var rb);

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce * ForceModifier, transform.position, _explosionRadius);
            }
        }
    }

    private void SetSeparationChance(int chance)
    {
        if (chance < _minChanceValue)
        {
            _separationChance = _minChanceValue;
        }
        else if (chance > _maxChanceValue)
        {
            _separationChance = _maxChanceValue;
        }
        else
        {
            _separationChance = chance;
        }
    }

    public ExplodableObject Clone()
    {
        var clone = Instantiate(this, transform.position, Quaternion.identity);

        clone.transform.localScale = transform.localScale / 2;

        clone.SetSeparationChance(_separationChance / 2);

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