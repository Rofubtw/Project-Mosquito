using System;
using Cysharp.Threading.Tasks;
using Fusion;
using System.Threading;
using UnityEngine;

public class PlayerInputHandler : PlayerModuleBase, IBeforeUpdate, IInputAccumulator
{
	[Networked]
	private NetworkButtons previousButtonData { get; set; }
	
	[Inject] private InputHandler inputHandler;
	
	// protected Vector2Accumulator _lookRotationAccumulator = new Vector2Accumulator(0.01f, true);
		
	private NetworkedInputData _accumulatedInput;
	private bool canControl = true;
	
	public Action<Vector2> OnLookInputReceived { get; set; }
	
	protected override UniTask InitializeModule(CancellationToken cancellationToken)
	{
		if (!HasInputAuthority)
		{
			this.enabled = false;
			return UniTask.CompletedTask;
		}

		var networkEvents = Runner.GetComponent<NetworkEvents>();

		networkEvents.OnInput.AddListener(OnInput);

		//ActionManager.OnMatchEnded += DisableControls;

		return UniTask.CompletedTask;
	}

	private void DisableControls()
	{
		canControl = false;
	}

	public override void SetupForLocalPlayer()
	{
		DependencyInjector.InjectInto(this);
		canControl = true;
	}

	private void Update()
	{
		if (!canControl)
			return;
		
		if (!inputHandler.IsInjected())
			return;
		
		GetMouseInput();
	}

	private void GetMouseInput()
	{
		Vector2 lookRotation = inputHandler.GetMouseInput();
		OnLookInputReceived?.Invoke(lookRotation);
	}

	public void BeforeUpdate()
	{
		if (!canControl)
			return;

		if (!inputHandler.IsInjected())
			return;

		if (!HasInputAuthority)
			return;

		AccumulateInput();
	}

	private void AccumulateInput()
	{
		_accumulatedInput = inputHandler.GetInput();

		bool isFastMovePressed = _accumulatedInput.Buttons.IsSet(InputButtonType.FastMove);
		_accumulatedInput.MoveDirection *= PlayerController.MovementController.GetMovementSpeed(isFastMovePressed);
	}

	private void OnInput(NetworkRunner runner, NetworkInput networkInput)
	{
        networkInput.Set(_accumulatedInput);

		_accumulatedInput = default;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		base.Despawned(runner, hasState);

		var networkEvents = Runner.GetComponent<NetworkEvents>();

		networkEvents.OnInput.RemoveListener(OnInput);
	}

	public bool IsFirstFrameOfInput(in NetworkButtons buttons, InputButtonType type) => buttons.WasPressed(previousButtonData, type);

	public bool IsButtonPressedContinuously(in NetworkButtons buttons, InputButtonType type) => buttons.IsSet(type);

	public void SetPreviousButtonData(in NetworkedInputData data) => previousButtonData = data.Buttons;

	bool IInputAccumulator.GetInput(out NetworkedInputData networkInputData) => GetInput(out networkInputData);

	public override void ResetModule() => previousButtonData = default;
}


public interface IInputAccumulator
{
	void SetPreviousButtonData(in NetworkedInputData data);

	bool IsFirstFrameOfInput(in NetworkButtons buttons, InputButtonType type);
	bool IsButtonPressedContinuously(in NetworkButtons buttons, InputButtonType type);

	bool GetInput(out NetworkedInputData networkInputData);
	
	Action<Vector2> OnLookInputReceived { get; set; }
}
