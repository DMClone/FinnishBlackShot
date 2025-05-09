using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    [SerializeField] private InputActionReference lookRef;
    private InputAction look;
    private InputDevice device;

    [SerializeField] private float maxRotation = 20f, speed = 4f;

    private Vector2 screenCenter;
    private Quaternion startRotation; // Store the initial rotation of the camera

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        look = playerInput.actions[lookRef.name];
    }

    private void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        startRotation = playerCamera.transform.localRotation; // Save the initial rotation
    }

    private void OnEnable()
    {
        look.started += OnLookPerformed;
    }

    private void OnDisable()
    {
        look.started -= OnLookPerformed;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        device = context.control.device;
        Debug.Log(device);
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
        Debug.Log(look.ReadValue<Vector2>());
    }

    private void MouseCameraMove()
    {
        // Get the mouse position in screen space
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Calculate the screen center
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Calculate the offset from the center
        Vector2 offset = mousePosition - screenCenter;

        // Normalize the offset to get a direction and scale it by speed
        Vector2 normalizedOffset = offset / screenCenter; // Normalize relative to screen size
        float rotationX = -normalizedOffset.y * speed; // Invert Y-axis for natural camera movement
        float rotationY = normalizedOffset.x * speed;

        // Clamp the rotation to the maximum allowed angles
        rotationX = Mathf.Clamp(rotationX, -maxRotation, maxRotation);
        rotationY = Mathf.Clamp(rotationY, -maxRotation, maxRotation);

        // Apply the rotation to the camera relative to the start rotation
        playerCamera.transform.localRotation = startRotation * Quaternion.Euler(rotationX, rotationY, 0f);
    }

    private void GamePadCameraMove()
    {
        // Read the gamepad input for rotation
        Vector2 gamePadRotation = look.ReadValue<Vector2>();

        // Scale the input by speed
        float rotationX = -gamePadRotation.y * speed; // Invert Y-axis for natural camera movement
        float rotationY = gamePadRotation.x * speed;

        // Clamp the rotation to the maximum allowed angles
        rotationX = Mathf.Clamp(rotationX, -maxRotation, maxRotation);
        rotationY = Mathf.Clamp(rotationY, -maxRotation, maxRotation);

        // Apply the rotation to the camera relative to the start rotation
        playerCamera.transform.localRotation = startRotation * Quaternion.Euler(rotationX, rotationY, 0f);
    }
}