using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public const int PressCode = 0;

    public Action Pressed;

    public Vector3 PressPosition => Input.mousePosition;

    private void Update()
    {
        bool isPressed = Input.GetMouseButtonDown(PressCode);

        if (isPressed)
        {
            Pressed?.Invoke();
        }
    }
}