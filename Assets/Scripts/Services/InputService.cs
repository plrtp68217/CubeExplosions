using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const int PressCode = 0;

    public event Action<Vector3> Pressed;

    public Vector3 PressPosition => Input.mousePosition;

    private void Update()
    {
        bool isPressed = Input.GetMouseButtonDown(PressCode);

        if (isPressed)
        {
            Pressed?.Invoke(PressPosition);
        }
    }
}