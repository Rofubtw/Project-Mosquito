using Cysharp.Threading.Tasks;
using Extensions;
using System.Threading;
using UnityEngine;

public abstract class PlayerModuleBase : InitializableNetworkComponent<PlayerController>, ICleanableOnDestroy
{
	public PlayerController PlayerController { get; private set; }

    [HideInInspector]
    [Inject] public GamePlayManagerBase gamePlayManager;

	protected PlayerPreferences playerPreferences;

	protected override sealed async UniTask OnInitialize(PlayerController playerController, CancellationToken cancellationToken)
	{
		if (cancellationToken.IsCancellationRequested)
			return;

		this.PlayerController = playerController;

		//playerPreferences = GameDataController.Instance.PlayerPreferences;

		DependencyInjector.InjectInto(this);

		await InitializeModule(cancellationToken);

		if (HasStateAuthority)
			SetupForLocalPlayer();
	}

	protected abstract UniTask InitializeModule(CancellationToken cancellationToken);

	public abstract void ResetModule();

	public virtual void SetupForLocalPlayer()
	{

	}

	public void CleanupOnDestroy()
	{
		this.ResetAllEventsAndDelegates();
	}
}
