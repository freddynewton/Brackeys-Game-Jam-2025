using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input using the new Input System
/// </summary>
public class InputManager : Singleton<InputManager>
{
    // Public properties to access the player input
    [HideInInspector] public Vector2 MoveInput;
    [HideInInspector] public Vector2 LookInput;

    // Public events for interaction actions
    [HideInInspector] public UnityEvent OnInteractStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnInteractPerformed = new UnityEvent();
    [HideInInspector] public UnityEvent OnInteractCanceled = new UnityEvent();
    [HideInInspector] public UnityEvent OnPrimaryInteractPerformed = new UnityEvent();
    [HideInInspector] public UnityEvent OnSecondaryInteractPerformed = new UnityEvent();

    // Serialized field to reference the InputActionAsset
    [SerializeField] private InputActionAsset _inputActions;

    // Private fields to store the player input actions
    private InputActionMap _playerInputActionMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;
    private InputAction _primaryInteractAction;
    private InputAction _secondaryInteractAction;

    private bool _isInitialized;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    public override void Awake()
    {
        base.Awake();

        _initialize();
    }

    public void SetPlayerInputActive(bool isActive)
    {
        if (isActive)
        {
            _moveAction.Enable();
            _primaryInteractAction.Enable();
            _secondaryInteractAction.Enable();
        }
        else
        {
            _moveAction.Disable();
            _primaryInteractAction.Disable();
            _secondaryInteractAction.Disable();
        }
    }

    /// <summary>
    /// Initializes the Input Manager by setting up the player input actions
    /// </summary>
    private void _initialize()
    {
        // Null check to prevent re-initialization
        if (_inputActions == null)
        {
            Debug.LogError("Input Actions not set in the Input Manager component on " + gameObject.name, this);
            return;
        }

        // Null check to prevent re-initialization
        if (_isInitialized)
        {
            return;
        }

        // Find the player input action map and the move and look actions
        _playerInputActionMap = _inputActions.FindActionMap("Player");
        _moveAction = _playerInputActionMap.FindAction("Move");
        _lookAction = _playerInputActionMap.FindAction("Look");
        _interactAction = _playerInputActionMap.FindAction("Interact");
        _primaryInteractAction = _playerInputActionMap.FindAction("PrimaryInteract");
        _secondaryInteractAction = _playerInputActionMap.FindAction("SecondaryInteract");

        _moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _moveAction.canceled += ctx => MoveInput = Vector2.zero;

        _lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _lookAction.canceled += ctx => LookInput = Vector2.zero;

        _interactAction.started += ctx => OnInteractStarted?.Invoke();
        _interactAction.performed += ctx => OnInteractPerformed?.Invoke();
        _interactAction.canceled += ctx => OnInteractCanceled?.Invoke();

        _primaryInteractAction.performed += ctx => OnPrimaryInteractPerformed?.Invoke();
        _secondaryInteractAction.performed += ctx => OnSecondaryInteractPerformed?.Invoke();

        // Enable the player input actions
        _inputActions.Enable();

        _isInitialized = true;
    }
}
