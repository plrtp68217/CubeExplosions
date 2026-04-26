using Assets.Scripts.Utils;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    [SerializeField] private ExplodableObjectsService _explodableObjectsService;

    private readonly int _minChanceValue = 0;
    private readonly int _maxChanceValue = 100;

    private int _separationChance = 100;

    public bool CanSeparating => RandomUtils.IsSuccess(_separationChance);
    public float ForceModifier => 1 / transform.localScale.x;

    private void OnEnable()
    {
        _explodableObjectsService.Register(this);
    }

    private void OnDisable()
    {
        _explodableObjectsService.UnRegister(this);
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

        Renderer renderer = clone.GetComponent<Renderer>();

        renderer.material.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );

        return clone;
    }
}