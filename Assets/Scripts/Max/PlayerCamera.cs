using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    private PlayerInputHandler playerInputHandler;
    private InputAction look;
    private InputDevice device;
    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        look = playerInputHandler.look;
    }

    private void OnEnable()
    {
        look.performed += OnLookPerformed;
    }

    private void OnDisable()
    {
        look.performed -= OnLookPerformed;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        device = context.control.device;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        switch (device)
        {
            case Mouse:
                MouseCameraMove();
                break;
            case Gamepad:
                GamePadCameraMove();
                break;
        }
    }

    private void MouseCameraMove()
    {
        
    }

    private void GamePadCameraMove()
    {

    }
}
