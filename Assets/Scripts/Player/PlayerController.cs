using Cysharp.Threading.Tasks;
using Extensions;
using Fusion;
using System;
using System.Linq;
using System.Threading;
using Photon.Realtime;
using UnityEngine;


[DefaultExecutionOrder(-5)]
public class PlayerController : SpawnableNetworkComponent, INetworkFixedUpdate
{
	public PlayerMovementModule MovementController { get; private set; }
	public PlayerCameraController CameraController { get; private set; }
	public PlayerHealthModule HealthController { get; private set; }
    public IInputAccumulator InputHandler { get; private set; }

	[Inject] public GamePlayManagerBase GamePlayManager;

	public Action OnAllModulesInitialized;
	public OnInputRecievedDelegate OnInputRecieved { get; set; }
	public delegate void OnInputRecievedDelegate(in NetworkedInputData networkInputData, bool hasInput = true);


	protected override async UniTask OnInitialize(CancellationToken token)
	{
		DependencyInjector.InjectInto(this);

#if UNITY_EDITOR
        name = $"{Object.InputAuthority} ({(HasInputAuthority ? "State Authority" : "Proxy")})";
#endif

		SetControllers();
		await InitializeModules(token);
		InitializeInputListeners();

		await UniTask.Delay(100);
	}

	private async UniTask InitializeModules(CancellationToken token)
	{
		PlayerModuleBase[] modules = GetComponentsInChildren<PlayerModuleBase>();
		
		while (modules.Any(m => m.IsSpawned == false))
		{
			token.ThrowIfCancellationRequested();
			await UniTask.Yield(token);
		}

		// while (modules.Contains(x => x.IsSpawned == false))
		// {
		// 	token.ThrowIfCancellationRequested();
		//
		// 	await UniTask.Yield(token);
		// }

		foreach (var module in modules)
		{
			token.ThrowIfCancellationRequested();
			await module.InitializeOnActionWithData(this, callback => OnAllModulesInitialized += callback);
		}

		CompleteInitialization();
	}


	private void InitializeInputListeners()
	{
		IInputListener[] inputListeners = GetComponentsInChildren<IInputListener>();
		
		foreach (var item in inputListeners)
		{
			item.RegisterInputListener();
		}
	}

	protected virtual void SetControllers()
	{
		MovementController = GetComponentInChildren<PlayerMovementModule>();

        CameraController = GetComponentInChildren<PlayerCameraController>();
        
        HealthController = GetComponentInChildren<PlayerHealthModule>();

		InputHandler = GetComponentInChildren<IInputAccumulator>();

#if UNITY_EDITOR
		CheckControllers();
#endif
	}

	private void CheckControllers()
	{
		if (MovementController == null) Debug.LogError("[PlayerController] PlayerMovementModule is not found.");
		if (CameraController == null) Debug.LogWarning("[PlayerController] PlayerCameraController is not found.");
		if (InputHandler == null) Debug.LogWarning("[PlayerController] IInputAccumulator is not found.");
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		base.Despawned(runner, hasState);

		foreach (var item in GetComponentsInChildren<ICleanableOnDestroy>())
			item.CleanupOnDestroy();
		
		OnInputRecieved = null;
		OnAllModulesInitialized = null;
		
		if (HasInputAuthority)
			ActionManager.LocalPlayer = null;
	}

	protected virtual void CompleteInitialization()
	{
		OnAllModulesInitialized?.Invoke();
		OnAllModulesInitialized = null;

		if (!HasInputAuthority) return;
		ActionManager.LocalPlayer = () => this;
		ActionManager.OnLocalPlayerSpawned?.Invoke(this);
	}

	public void NetworkFixedUpdate()
	{
		//if (HealthController.IsAlive == false)
		//{
		//	ProcessNoInput();
		//	return;
		//}
		
		if (InputHandler == null)
			return;
		

		if (InputHandler.GetInput(out NetworkedInputData networkInputData))
		{
            ProcessInput(networkInputData);
		}
		else
		{
			ProcessNoInput();
		}
	}

	private void ProcessNoInput()
	{
		OnInputRecieved?.Invoke(default, false);
		InputHandler?.SetPreviousButtonData(default); // Check this for a bug
	}

	private void ProcessInput(in NetworkedInputData networkInputData)
	{
		OnInputRecieved?.Invoke(networkInputData);
		InputHandler.SetPreviousButtonData(in networkInputData);
	}

	public bool IsFirstFrameOfInput(in NetworkButtons buttons, InputButtonType type)
	{
		return InputHandler.IsFirstFrameOfInput(buttons, type);
	}

	public bool IsButtonPressedContinuously(in NetworkButtons buttons, InputButtonType type)
	{
		return InputHandler.IsButtonPressedContinuously(buttons, type);
	}
}

public static partial class ActionManager
{
	public static Func<PlayerController> LocalPlayer;
	public static Action<PlayerController> OnLocalPlayerSpawned;
}