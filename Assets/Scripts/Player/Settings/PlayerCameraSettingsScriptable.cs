using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Bumblebit/ScriptableObjects/Player/CameraSettings", fileName = "CameraSettings")]
public class PlayerCameraSettingsScriptable : ScriptableObject
{
    // Priority
    [FoldoutGroup("Priority")] public int LocalPriority = 100;
    [FoldoutGroup("Priority")] public int RemotePriority = 1;

    // Sensitivity
    [FoldoutGroup("Sensitivity")] public float YawSensitivity = 12f;   // deg/sec
    [FoldoutGroup("Sensitivity")] public float PitchSensitivity = 12f; // deg/sec
    [FoldoutGroup("Sensitivity")] public bool InvertY = false;

    // Time / Delta
    [FoldoutGroup("Time")] public bool UseUnscaledTime = true;
    [FoldoutGroup("Time")] public bool ApplyDeltaTime = true;

    // Pitch limits
    [FoldoutGroup("Pitch")] public bool ClampPitch = true;
    [FoldoutGroup("Pitch")] public float PitchMin = -10f;
    [FoldoutGroup("Pitch")] public float PitchMax = 45f;

    // Smoothing
    [FoldoutGroup("Smoothing")] [Min(0f)] public float LookSmoothing = 0f; // exponential smoothing factor

    // Cinemachine specifics
    [FoldoutGroup("Cinemachine")] public bool DisableRecentering = true;
}
