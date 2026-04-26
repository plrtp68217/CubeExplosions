using Assets.Scripts.Utils;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    private readonly int _minChanceValue = 0;
    private readonly int _maxChanceValue = 100;

    private int _separationChance = 100;

    public bool CanSeparate => RandomUtils.IsSuccess(_separationChance);
    public float ForceModifier => 1 / transform.localScale.x;

    public ExplodableObject Clone()
    {
        var clone = Instantiate(this, transform.position, Quaternion.identity);

        clone.transform.localScale = transform.localScale / 2;

        clone.SetSeparationChance(_separationChance / 2);

        Renderer renderer = clone.GetComponent<Renderer>();

        renderer.material.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );

        return clone;
    }

    private void SetSeparationChance(int chance)
    {
        _separationChance = Mathf.Clamp(chance, _minChanceValue, _maxChanceValue);
    }
}