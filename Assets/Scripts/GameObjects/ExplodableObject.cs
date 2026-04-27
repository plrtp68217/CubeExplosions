using Assets.Scripts.Utils;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    private readonly int _minChanceValue = 0;
    private readonly int _maxChanceValue = 100;

    private int _separationChance = 100;

    public int SeparationChance => _separationChance;
    public bool CanSeparate => RandomUtils.IsSuccess(_separationChance);

    public void ApplyExplosion(Vector3 position, float force, float radius)
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddExplosionForce(force / transform.localScale.x, position, radius);
        }
    }

    public void SetSeparationChance(int chance)
    {
        _separationChance = Mathf.Clamp(chance, _minChanceValue, _maxChanceValue);
    }

    public void SetRandomColor()
    {
        if (TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}