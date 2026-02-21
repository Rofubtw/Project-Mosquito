using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerInteractionModule : PlayerModuleBase
{
    protected override UniTask InitializeModule(CancellationToken cancellationToken)
    {
        return UniTask.CompletedTask;
    }

    public override void ResetModule()
    {
        
    }
}
