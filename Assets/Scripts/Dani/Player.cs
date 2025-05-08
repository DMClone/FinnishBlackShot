using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput _playerInput;

    private void OnEnable()
    {
        InputAction PlayerPlay = InputSystem.actions.FindAction("Play");
        PlayerPlay.performed += ActionOne;
        InputAction PlayerPass = InputSystem.actions.FindAction("Pass");
        PlayerPass.performed += ActionOne;
    }

    public void ActionOne(InputAction.CallbackContext context)
    {
        // Do action
    }

    public void ActionTwo(InputAction.CallbackContext context)
    {
        // Do action
    }
}
