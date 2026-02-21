
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : MonoBehaviour, INetworkRunnerCallbacks
{
    [field: SerializeField] public FusionConfig Config { get; private set; }

    public bool IsConnected { get; private set; }
    public NetworkRunner Runner { get; private set; }

    public IReadOnlyList<SessionInfo> LatestSessions => latestSessions;
    private List<SessionInfo> latestSessions = new();

    private SessionLobby lastJoinedLobby = SessionLobby.Shared; // remembers last lobby
    private double lastRefreshTime;                             // simple debounce
    
    private const double RefreshCooldownSeconds = 1.0;
    private const int DefaultPlayerCount = 2;

    public event Action<IReadOnlyList<SessionInfo>> SessionListUpdated;

    private void Awake()
    {
        GameServiceRegistry.Register(this);
    }

    public async UniTask JoinLobbyAsync()
    {
        EnsureRunner();
        lastJoinedLobby = SessionLobby.Shared;
        await Runner.JoinSessionLobby(lastJoinedLobby);
    }

    public async UniTask JoinLobbyAsync(SessionLobby lobby)
    {
        EnsureRunner();
        lastJoinedLobby = lobby;
        await Runner.JoinSessionLobby(lastJoinedLobby);
    }

    public async UniTask RefreshSessionsAsync()
    {
        // Simple debounce to avoid spamming the transport
        if (Time.realtimeSinceStartupAsDouble - lastRefreshTime < RefreshCooldownSeconds)
            return;

        lastRefreshTime = Time.realtimeSinceStartupAsDouble;

        EnsureRunner();
        // Re-join the same lobby to trigger a fresh session list update
        await Runner.JoinSessionLobby(lastJoinedLobby);
        // OnSessionListUpdated will fire if there are changes
    }

    public async UniTask<StartGameResult> StartSharedAsync(string sessionName, SceneRef scene, Dictionary<string, SessionProperty> properties = null, int? playerCount = null)
    {
        EnsureRunner();

        StartGameArgs gameArgs = new StartGameArgs 
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            Scene = scene,
            SessionProperties = properties,
            PlayerCount = playerCount ?? DefaultPlayerCount
        };

        StartGameResult result = await Runner.StartGame(gameArgs);

        return result;
    }

    public UniTask<StartGameResult> StartSharedBySceneNameAsync(string sessionName, string sceneName, Dictionary<string, SessionProperty> properties = null, int? playerCount = null)
    {
        var scene = BuildSceneRefByName(sceneName);
        return StartSharedAsync(sessionName, scene, properties, playerCount);
    }

    public async UniTask ShutdownAsync()
    {
        if (Runner == null) return;
        try
        {
            await Runner.Shutdown();
        }
        finally
        {
            if (Runner != null)
            {
                Runner.RemoveCallbacks(this);
                UnityEngine.Object.Destroy(Runner.gameObject);
                Runner = null;
            }
            IsConnected = false;
        }
    }

    // --- Helpers ---

    private void EnsureRunner()
    {
        CreateNetworkRunner();

        Runner.AddCallbacks(this);
    }

    private void CreateNetworkRunner()
    {
        if (Runner != null)
        {
            Runner.RemoveCallbacks();
            Destroy(Runner.gameObject);
        }


        Runner = Instantiate(Config.runnerPrefab.gameObject).GetComponent<NetworkRunner>();
        Runner.ProvideInput = true;
    }

    private static SceneRef BuildSceneRefByName(string sceneName)
    {
        // Find build index by scene name (Build Settings required)
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return SceneRef.FromIndex(i);
        }

        Debug.LogError($"[FusionRunnerService] Scene '{sceneName}' not found in Build Settings.");
        return SceneRef.None;
    }

    #region Runner Callbacks

    // --- INetworkRunnerCallbacks ---

    public void OnConnectedToServer(NetworkRunner runner)
    {
        IsConnected = true;
        //Connected?.Invoke();
    }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        IsConnected = false;
        //Disconnected?.Invoke();
    }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogWarning($"[FusionRunnerService] ConnectFailed: {reason}");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        latestSessions = sessionList?.ToList() ?? new List<SessionInfo>();
        SessionListUpdated?.Invoke(latestSessions);
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        //SceneLoadStarted?.Invoke();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        //SceneLoadCompleted?.Invoke();
    }

    // Unused callbacks (kept for completeness)
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    #endregion
}
