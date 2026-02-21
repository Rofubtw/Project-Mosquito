using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreationHandler : MonoBehaviour
{
    [Inject] public ConnectionHandler connectionHandler;

    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Toggle passwordToggle;
    [SerializeField] private Slider maxPlayersSlider;
    [SerializeField] private TextMeshProUGUI maxPlayersCountText;
    [SerializeField] private Button createButton;

    private string RoomName => roomNameInput.text;
    private string Password => passwordInput.text;
    private int MaxPlayers => Mathf.RoundToInt(maxPlayersSlider.value);
    private bool PasswordEnabled => passwordToggle.isOn;

    private void Awake()
    {
        DependencyInjector.InjectInto(this);

        passwordToggle.onValueChanged.AddListener(OnPasswordToggle);

        createButton.onClick.AddListener(OnCreateButtonClicked);

        maxPlayersSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));

        OnPasswordToggle(PasswordEnabled);
    }

    private void OnDestroy()
    {
        passwordToggle.onValueChanged.RemoveAllListeners();
        createButton.onClick.RemoveAllListeners();
    }

    private void OnPasswordToggle(bool on) => passwordInput.interactable = on;

    private void OnSliderValueChanged(float newValue) => maxPlayersCountText.text = Mathf.RoundToInt(newValue).ToString();

    private async void OnCreateButtonClicked()
    {
        string roomName = string.IsNullOrWhiteSpace(RoomName)
            ? $"Room-{UnityEngine.Random.Range(1000, 9999)}"
            : RoomName;

        string password = PasswordEnabled ? Password : string.Empty;

        //ActionManager.OnLoadingStarted?.Invoke();
        //loading.Show();
        var result = await CreateRoomAsync(roomName, MaxPlayers, password);
        //loading.Hide();

        if (!result.Success)
        {
            //ActuinManager.OnShowPopup?.Invoke(result.Error ?? "Create failed.");
            //popup.Show(result.Error ?? "Create failed.");
        }
    }

    public async UniTask<RoomCreateResult> CreateRoomAsync(string roomName, int maxPlayers, string password)
    {
        var props = new Dictionary<string, SessionProperty> {{ "password", password ?? string.Empty }};

        StartGameResult result = await connectionHandler.StartSharedBySceneNameAsync(roomName, connectionHandler.Config.gameSceneName, props, maxPlayers);

        if (!result.Ok)
            return RoomCreateResult.Fail(result.ShutdownReason.ToString(), result);

        return RoomCreateResult.Ok(result);
    }
}
