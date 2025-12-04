using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; }

    private InputActions inputActions;

    public event EventHandler OnMenuButtonPressed;

    private void Awake()
    {
        Instance = this;    
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.MenuAction.performed += MenuAction_performed;
    }

    private void MenuAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMenuButtonPressed?.Invoke(this,EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }
    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }
    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }
    public Vector2 GetMovementInputVector()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
