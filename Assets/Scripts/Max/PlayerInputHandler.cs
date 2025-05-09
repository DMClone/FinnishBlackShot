using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private InputActionReference hitRef, pasRef, leftArrowRef, rightArrowRef, lookRef;
    [HideInInspector] public InputAction hit, pas, look, leftArrow, rightArrow;

    // Events for the actions
    public event System.Action<GameObject> OnHitPerformed;
    public event System.Action<GameObject> OnPasPerformed;
    public event System.Action<GameObject, int> OnLeftArrowPerformed;
    public event System.Action<GameObject, int> OnRightArrowPerformed;
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
        leftArrow.performed += OnLeftArrowAction; // Use dedicated method for left arrow
        rightArrow.performed += OnRightArrowAction; // Use dedicated method for right arrow
        look.performed += context => HandleAction(context, OnLookPerformed);

        // Subscribe to GameManager methods
        OnHitPerformed += GameManager.Instance.CheckDraw;
        OnPasPerformed += GameManager.Instance.Stand;
        OnLeftArrowPerformed += GameManager.Instance.AceValue;
        OnRightArrowPerformed += GameManager.Instance.AceValue;
    }

    private void OnDisable()
    {
        // Unsubscribe from input action events
        hit.performed -= context => HandleAction(context, OnHitPerformed);
        pas.performed -= context => HandleAction(context, OnPasPerformed);
        leftArrow.performed -= OnLeftArrowAction; // Unsubscribe dedicated method
        rightArrow.performed -= OnRightArrowAction; // Unsubscribe dedicated method
        look.performed -= context => HandleAction(context, OnLookPerformed);
    }

    private void HandleAction(InputAction.CallbackContext context, System.Action<GameObject> action)
    {
        if (!GameStarted()) return;
        action?.Invoke(gameObject);
    }

    private void OnLeftArrowAction(InputAction.CallbackContext context)
    {
        if (!GameStarted()) return;
        // Invoke the event with a hardcoded value of 1
        OnLeftArrowPerformed?.Invoke(gameObject, 1);
    }

    private void OnRightArrowAction(InputAction.CallbackContext context)
    {
        if (!GameStarted()) return;
        // Invoke the event with a hardcoded value of 11
        OnRightArrowPerformed?.Invoke(gameObject, 11);
    }

    private void HandleAction(InputAction.CallbackContext context, System.Action<Vector2> action)
    {
        if (!GameStarted()) return;
        if (action != null)
        {
            Vector2 input = context.ReadValue<Vector2>();
            action.Invoke(input);
        }
    }

    private bool GameStarted()
    {
        return GameManager.Instance.GameStarted();
    }
}