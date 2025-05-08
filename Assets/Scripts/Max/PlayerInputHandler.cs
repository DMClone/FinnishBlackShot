using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private InputActionReference hitRef, pasRef, leftArrowRef, rightArrowRef, lookRef;
    [HideInInspector] public InputAction hit, pas, look, leftArrow, rightArrow;

    // Events for the actions
    public event System.Action OnHitPerformed;
    public event System.Action OnPasPerformed;
    public event System.Action OnLeftArrowPerformed;
    public event System.Action OnRightArrowPerformed;
    public event System.Action<Vector2> OnLookPerformed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // Initialize actions
        hit = playerInput.actions[hitRef.name];
        pas = playerInput.actions[pasRef.name];
        look = playerInput.actions[lookRef.name];
        leftArrow = playerInput.actions[leftArrowRef.name];
        rightArrow = playerInput.actions[rightArrowRef.name];
    }

    private void OnEnable()
    {
        // Subscribe to input action events
        hit.performed += context => HandleAction(context, OnHitPerformed);
        pas.performed += context => HandleAction(context, OnPasPerformed);
        leftArrow.performed += context => HandleAction(context, OnLeftArrowPerformed);
        rightArrow.performed += context => HandleAction(context, OnRightArrowPerformed);
        look.performed += context => HandleAction(context, OnLookPerformed);
    }

    private void OnDisable()
    {
        // Unsubscribe from input action events
        hit.performed -= context => HandleAction(context, OnHitPerformed);
        pas.performed -= context => HandleAction(context, OnPasPerformed);
        leftArrow.performed -= context => HandleAction(context, OnLeftArrowPerformed);
        rightArrow.performed -= context => HandleAction(context, OnRightArrowPerformed);
        look.performed -= context => HandleAction(context, OnLookPerformed);
    }

    private void HandleAction(InputAction.CallbackContext context, System.Action action)
    {
        action?.Invoke();
    }

    private void HandleAction(InputAction.CallbackContext context, System.Action<Vector2> action)
    {
        if (action != null)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Debug.Log(input);
            action.Invoke(input);
        }
    }
}