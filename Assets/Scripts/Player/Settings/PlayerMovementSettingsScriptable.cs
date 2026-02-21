using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Bumblebit/ScriptableObjects/Player/MovementSettings", fileName = "MovementSettings")]
public class PlayerMovementSettingsScriptable : ScriptableObject
{
    [FoldoutGroup("Speed")] public float MovementSpeed = 4.0f;

    [FoldoutGroup("Speed")] public float FastMovementSpeed = 10f;

    [FoldoutGroup("Speed")] public float FlySpeedMultiplier = 3f;

    [FoldoutGroup("Speed")] public float RotationSpeed = 80f;
    
    [FoldoutGroup("Gravity")] public float UpGravity = 10f;

    [FoldoutGroup("Gravity")] public float DownGravity = 15f;

    [FoldoutGroup("Gravity")] public float DownGravityMultiplier = 2f; // For fast falling

    [FoldoutGroup("Acceleration")] public float AirAcceleration = 15f;

    [FoldoutGroup("Acceleration")] public float AirDeceleration = 1f;

    [FoldoutGroup("Acceleration")] public float GroundAcceleration = 10f;

    [FoldoutGroup("Acceleration")] public float GroundDeceleration = 10f;

    // Fly (free flight)
    [FormerlySerializedAs("FlyThrustPerSecond"), FoldoutGroup("Fly")] public float FlyUpThrustPerSecond = 100f;
    [FoldoutGroup("Fly")] public float FlyDownThrustPerSecond = 10f;
    [FoldoutGroup("Fly")] public float FlyMaxUpSpeed = 10f;
    [FoldoutGroup("Fly")] public float FlyMaxDownSpeed = 8f; // clamp downward speed in free flight
    public float GetGravity(bool isRising)
    {
        return isRising ? -UpGravity : -DownGravity;
    }
}