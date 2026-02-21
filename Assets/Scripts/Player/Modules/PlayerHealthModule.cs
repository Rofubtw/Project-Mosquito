using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealthModule : PlayerModuleBase, IDamagable
{
    [SerializeField] private PlayerHealthSettingsScriptable healthSettings;
    
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    private float CurrentHealth { get; set; }

    private float lastHealth;
    private bool isVincible => true;
    
    #region PROPERTIES
    
    public float MaxHealth { get; private set; }
    
    public bool IsAlive => CurrentHealth > 0;
    
    #endregion
    
    #region ACTIONS
    public delegate void HealthUpdateDelegate(float oldValue, float newValue, float maxValue);
    public HealthUpdateDelegate OnHealthUpdated { get; set; }
    public Action<PlayerController> OnPlayerGetHit { get; set; }
    public Action OnDied { get; set; }

    #endregion
    
    protected override UniTask InitializeModule(CancellationToken cancellationToken)
    {
        SetupHealth();

        return UniTask.CompletedTask;
    }
    
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);

        OnHealthUpdated = null;

        OnDied = null;
    }

    public override void ResetModule()
    {
        SetupHealth();
    }
    
    public void Heal(float healAmount)
    {
        if (!IsAlive)
            return;

        UpdateHealth(healAmount);
    }
    
    public void TakeDamage(AttackInfo attackInfo, OnEnemyDamagedDelegate OnHit)
    {
        if (!isVincible)
        {
            OnHit?.Invoke(false);
            return;
        }

        bool isDead = (CurrentHealth - attackInfo.Damage) <= 0f;

        OnHit?.Invoke(isDead);

        if (!HasStateAuthority)
            return;

        UpdateHealth(-attackInfo.Damage);

        if (isDead)
        {
            ActionManager.OnPlayerKilled?.Invoke(new KillData()
            {
                victim = Object.InputAuthority,
                killer = attackInfo.Attacker,
            });
        }
    }
    
    private void SetupHealth()
    {
        MaxHealth = healthSettings.MaxHealth;

        lastHealth = CurrentHealth;

        UpdateHealth(MaxHealth);
    }
    
    private void UpdateHealth(float addition)
    {

        if (!HasStateAuthority)
            return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + addition, 0f, healthSettings.MaxHealth);
    }
    
    private void OnHealthChanged()
    {
        if (IsAlive == false)
        {
            OnDied?.Invoke();
        }

        OnHealthUpdated?.Invoke(lastHealth, CurrentHealth, healthSettings.MaxHealth);

        lastHealth = CurrentHealth;
    }
    
    
}

public static partial class ActionManager
{
    public static Action<KillData> OnPlayerKilled { get; set; }

}

public struct KillData
{
    public PlayerRef killer;
    public PlayerRef victim;
}


public delegate void OnEnemyDamagedDelegate(bool killed);

public interface IDamagable
{
    void TakeDamage(AttackInfo attackInfo, OnEnemyDamagedDelegate OnHit);

    bool IsAlive { get; }
}

[Serializable]
public class AttackInfo
{
    public PlayerRef Attacker;
    public float Damage;
    public bool IsHeadshot;

    public AttackInfo(PlayerRef attacker, float damage, bool isHeadshot)
    {
        Attacker = attacker;
        Damage = damage;
        IsHeadshot = isHeadshot;
    }
}