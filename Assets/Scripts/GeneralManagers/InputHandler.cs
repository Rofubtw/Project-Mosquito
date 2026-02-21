using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IInputController
{
	public bool ResetInput { get; set; }

    private NetworkedInputData inputData;
    private bool readInput;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

	public void Activate(bool active)
	{
		// TODO : Implement activation/deactivation logic if needed
	}

	private void Awake() => GameServiceRegistry.Register(this);

    public NetworkedInputData GetInput()
    {
        if (ResetInput)
        {
            ResetInput = false;
            inputData = default;
        }

        HandleCursorLockToggle();

        if (!readInput)
        {
            inputData = default;
            return inputData;
        }

        GetKeyboardInputs();

        return inputData;
    }

    private void GetKeyboardInputs()
    {
        var keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        Vector2 moveDirection = Vector2.zero;

        if (keyboard.wKey.isPressed) moveDirection += Vector2.up;
        if (keyboard.sKey.isPressed) moveDirection += Vector2.down;
        if (keyboard.aKey.isPressed) moveDirection += Vector2.left;
        if (keyboard.dKey.isPressed) moveDirection += Vector2.right;

        inputData.MoveDirection = moveDirection.normalized;

        inputData.Buttons.Set(InputButtonType.FlyUp, keyboard.spaceKey.isPressed);
        inputData.Buttons.Set(InputButtonType.FlyDown, keyboard.ctrlKey.isPressed);
        inputData.Buttons.Set(InputButtonType.FlyStayMode, keyboard.fKey.isPressed);
        inputData.Buttons.Set(InputButtonType.FastMove, keyboard.leftShiftKey.isPressed);
    }

    public Vector2 GetMouseInput()
    {
        if (!readInput)
            return Vector2.zero;
        
        var mouse = Mouse.current;

        if (mouse == null)
            return Vector2.zero;

        Vector2 mouseDelta = mouse.delta.ReadValue();

        Vector2 lookRotationDelta = new Vector2(-mouseDelta.y, mouseDelta.x);
        // Vector2 lookRotationDelta = new Vector2(-mouseDelta.y, mouseDelta.x) / 1000;

        return lookRotationDelta;
    }

    private void HandleCursorLockToggle()
    {
        var keyboard = Keyboard.current;

        if (keyboard != null && (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame || keyboard.escapeKey.wasPressedThisFrame))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                readInput = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                readInput = true;
            }
        }
    }
	
}

public struct NetworkedInputData : INetworkInput
{
    public NetworkButtons Buttons;
    public Vector2 MoveDirection;
}

public enum InputButtonType
{
    FlyUp,
    FlyDown,
    FlyStayMode,
    FastMove,
    Interact,
}

public interface IInputController
{
    void Initialize();
    NetworkedInputData GetInput();
	void Activate(bool active);

}

public interface IInputListener
{
	void RegisterInputListener();

}