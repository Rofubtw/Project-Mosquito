using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class SpawnableNetworkComponent : NetworkComponentBase
{
	public override void Spawned()
	{
		base.Spawned();

		Initialize();
	}
	
	protected virtual async void Initialize()
	{
		while (isSpawned == false)
		{
			cancellationTokenSource.Token.ThrowIfCancellationRequested();

			await UniTask.Yield(cancellationTokenSource.Token);
		}

		if (cancellationTokenSource.IsCancellationRequested)
			return;

		await OnInitialize(cancellationTokenSource.Token);

		OnComponentReady();
	}

	protected abstract UniTask OnInitialize(CancellationToken token);
}
