using Assets.Scripts.Types;
using UnityEngine;

public class PressHandler : MonoBehaviour
{
    [SerializeField] private InputService _inputService;

    [SerializeField] private CustomLayer _targetLayer = CustomLayer.RaycastTarget;
    [SerializeField] private float _maxDistance = Mathf.Infinity;

    private LayerMask _hitLayers;

    private void Awake()
    {
        _hitLayers = LayerMask.GetMask(_targetLayer.ToString());
    }

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

        Ray ray = Camera.main.ScreenPointToRay(pressPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _hitLayers))
        {
            Debug.Log(hit);
        }
    }
}
