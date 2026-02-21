using Fusion;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// Base class for all network components that want to hook into Fusion's fixed update and render cycles.
/// Interfaces <see cref="INetworkFixedUpdate"/> and <see cref="INetworkRender"/> can be implemented
/// to receive respective update calls.
/// </summary>
public abstract class NetworkComponentBase : NetworkBehaviour
{
	protected bool isSpawned = false;

	protected bool isInitialized = false;

	private event Action OnFixedUpdateLoop;

	private event Action OnRenderLoop;

	public bool IsSpawned => isSpawned;

	public bool IsInitialized => isInitialized;

	protected CancellationTokenSource cancellationTokenSource;

	public override void Spawned()
	{
		cancellationTokenSource = new CancellationTokenSource();

		base.Spawned();

		isSpawned = true;
	}

	protected void OnComponentReady()
	{
		if (this is INetworkFixedUpdate networkFixedUpdate)
		{
			OnFixedUpdateLoop += networkFixedUpdate.NetworkFixedUpdate;
		}

		if (this is INetworkRender networkRender)
		{
			OnRenderLoop += networkRender.NetworkRender;
		}

		isInitialized = true;
	}

	public sealed override void FixedUpdateNetwork()
	{
		base.FixedUpdateNetwork();

		OnFixedUpdateLoop?.Invoke();
	}

	public sealed override void Render()
	{
		base.Render();

		OnRenderLoop?.Invoke();
	}


	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		cancellationTokenSource.Cancel();

		OnFixedUpdateLoop = null;

		OnRenderLoop = null;

		isSpawned = false;

		isInitialized = false;

		base.Despawned(runner, hasState);
	}
}

public interface INetworkFixedUpdate
{
	void NetworkFixedUpdate();
}


public interface INetworkRender
{
	void NetworkRender();
}

public interface ICleanableOnDestroy
{
	void CleanupOnDestroy();
}