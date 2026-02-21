using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListHandler : MonoBehaviour
{
    [Inject] private ConnectionHandler connectionHandler;

    [SerializeField] private RoomEntryView entryPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private GameObject noRoomsLabel;
    [SerializeField] private Button refreshButton;

    private readonly List<RoomEntryView> pool = new();
    private string filter = string.Empty;
    private List<RoomViewModel> cache = new();

    private void Awake()
    {
        DependencyInjector.InjectInto(this);

        searchInput.onValueChanged.AddListener(SearchChanged);
        refreshButton.onClick.AddListener(RefreshClicked);
    }

    private void Start()
    {
        connectionHandler.SessionListUpdated += OnSessionListUpdated;
        RebuildRooms(connectionHandler.LatestSessions);
    }

    private void OnDestroy()
    {
        searchInput?.onValueChanged.RemoveAllListeners();
        refreshButton?.onClick.RemoveAllListeners();

        foreach (var item in pool)
        {
            if (item == null) continue;
            item.JoinRequested -= HandleJoinRequested;
            item.PasswordSubmitted -= HandlePasswordSubmitted;
        }
    }

    private void SearchChanged(string text) => SetFilter(text);

    private async void RefreshClicked()
    {
        // TODO : Need a loading indicator here
        //loading.Show();
        await connectionHandler.RefreshSessionsAsync();
        //loading.Hide();
        // Optional: you could diff and show "X new rooms" via popup if desired.
    }

    private void OnRoomsChanged(IReadOnlyList<RoomViewModel> rooms)
    {
        // Ensure pool size
        while (pool.Count < rooms.Count)
        {
            var item = Instantiate(entryPrefab, content);
            item.gameObject.SetActive(false);
            item.JoinRequested += HandleJoinRequested;
            item.PasswordSubmitted += HandlePasswordSubmitted;
            pool.Add(item);
        }

        // Fill visible entries
        for (int i = 0; i < rooms.Count; i++)
        {
            var item = pool[i];
            item.Set(rooms[i]);
            item.gameObject.SetActive(true);
        }

        // Hide extras
        for (int i = rooms.Count; i < pool.Count; i++)
            pool[i].gameObject.SetActive(false);

        noRoomsLabel.SetActive(rooms.Count == 0);
    }

    private void HandleJoinRequested(string roomName) => Join(roomName, null).Forget();

    private void HandlePasswordSubmitted(string roomName, string password) => Join(roomName, password).Forget();

    private async UniTaskVoid Join(string roomName, string passwordOrNull)
    {
        // TODO: Show loading indicator
        //loading.Show();
        RoomJoinResult result = await JoinRoomAsync(roomName, passwordOrNull);
        //loading.Hide();

        if (!result.Success)
        {
            // TODO: Show popup
            //if (result.Error == "WrongPassword")
            //    popup.Show("Wrong password.");
            //else
            //    popup.Show(result.Error ?? "Join failed.");
        }
    }

    private async UniTask<RoomJoinResult> JoinRoomAsync(string sessionName, string providedPasswordOrNull)
    {
        var foundSession = connectionHandler.LatestSessions.FirstOrDefault(s => s.Name == sessionName);
        bool hasSession = !string.IsNullOrEmpty(foundSession.Name);

        if (hasSession)
        {
            bool hasPass = foundSession.Properties.TryGetValue("password", out var passProp) && !string.IsNullOrEmpty(passProp);
            if (hasPass)
            {
                if (string.IsNullOrEmpty(providedPasswordOrNull) || providedPasswordOrNull != passProp)
                {
                    return RoomJoinResult.Fail("WrongPassword");
                }
            }
        }

        var result = await connectionHandler.StartSharedBySceneNameAsync(sessionName, connectionHandler.Config.gameSceneName);

        if (!result.Ok)
            return RoomJoinResult.Fail(result.ShutdownReason.ToString(), result);

        return RoomJoinResult.Ok(result);
    }

    private void SetFilter(string query)
    {
        filter = query ?? string.Empty;
        RebuildRooms(connectionHandler.LatestSessions);
    }

    private void OnSessionListUpdated(IReadOnlyList<SessionInfo> sessions)
    {
        RebuildRooms(sessions);
    }

    private void RebuildRooms(IReadOnlyList<SessionInfo> sessions)
    {
        var list = sessions?.Select(s => new RoomViewModel(s)).ToList() ?? new List<RoomViewModel>();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            var f = filter.Trim();
            list = list.Where(r => r.Name?.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        cache = list;
        OnRoomsChanged(cache);
    }
}
