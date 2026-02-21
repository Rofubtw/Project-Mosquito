using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public abstract class InitializableNetworkComponent<T> : NetworkComponentBase
{
	public async UniTask InitializeWithData(T initData)
	{
		while (isSpawned == false)
		{
			cancellationTokenSource.Token.ThrowIfCancellationRequested();

			await UniTask.Yield(cancellationTokenSource.Token);
		}

		if (cancellationTokenSource.IsCancellationRequested)
			return;

		await OnInitialize(initData, cancellationTokenSource.Token);

		OnComponentReady();
	}

	public async UniTask InitializeOnActionWithData(T initData, Action<Action> registerReadyCallback)
	{
		while (isSpawned == false)
		{
			cancellationTokenSource.Token.ThrowIfCancellationRequested();

			await UniTask.Yield(cancellationTokenSource.Token);
		}

		if (cancellationTokenSource.IsCancellationRequested)
			return;

		await OnInitialize(initData, cancellationTokenSource.Token);

		// Register this module's ready event to external master event
		registerReadyCallback?.Invoke(OnComponentReady);
	}

	protected abstract UniTask OnInitialize(T initData, CancellationToken cancellationToken);

}