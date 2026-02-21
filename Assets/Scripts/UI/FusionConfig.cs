using Fusion;
using UnityEngine;

[CreateAssetMenu(menuName = "Bumblebit/ScriptableObjects/Connection/Fusion Config", fileName = "FusionConfig")]
public class FusionConfig : ScriptableObject
{
    [Header("Runner")]
    [Tooltip("NetworkRunner prefab to be injected into the scene")]
    public NetworkRunner runnerPrefab;

    [Header("Scenes")]
    [Tooltip("Name of the game scene (must be in Build Settings)")]
    public string gameSceneName = "Game";

    [Tooltip("Optional scene to load additively after the game scene")]
    public string additiveSceneName;

    [Tooltip("Should the additive scene be loaded by the authority?")]
    public bool loadAdditiveOnAuthority = true;


    //[Header("Misc")]
    //[Tooltip("Should the runner object persist between scenes?")]
    //public bool dontDestroyOnLoad = true;




    //[Header("Lobby / Listing")]
    //[Tooltip("Room list refresh interval (usually not needed; Fusion sends events)")]
    //public float refreshIntervalSeconds = 2f;
}
