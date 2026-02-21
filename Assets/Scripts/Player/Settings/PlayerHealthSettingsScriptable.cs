using UnityEngine;

[CreateAssetMenu(menuName = "Bumblebit/ScriptableObjects/Player/HealthSettings", fileName = "HealthSettings")]
public class PlayerHealthSettingsScriptable : ScriptableObject
{
    public float MaxHealth = 100f;
    
    [Range(0, 100)]
    public int LowHealthThreshold = 30;
}