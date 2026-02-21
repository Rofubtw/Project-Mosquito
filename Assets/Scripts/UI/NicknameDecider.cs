using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NicknameDecider : MonoBehaviour
{
	[SerializeField, FoldoutGroup("Panels")] private GameObject namePanelRoot;   // The first screen (nickname)
	[SerializeField, FoldoutGroup("Panels")] private GameObject lobbyPanelRoot;  // The second screen (room list + create)

	[SerializeField, FoldoutGroup("UI")] private TMP_InputField nicknameInput;
	[SerializeField, FoldoutGroup("UI")] Button confirmButton;

	public string Nickname { get; private set; }

	public bool HasNickname => !string.IsNullOrWhiteSpace(Nickname);


	private void Awake()
	{
		LoadFromPrefs();

		confirmButton.onClick.AddListener(EmitConfirm);

		// Enter key support
		nicknameInput.onSubmit.AddListener(_ => EmitConfirm());
		nicknameInput.onValueChanged.AddListener(OnNameChanged);
	}
	private void Start()
	{
		DecidePanels();
	}

	private void OnDestroy()
	{
		confirmButton?.onClick.RemoveAllListeners();
		nicknameInput.onSubmit.RemoveAllListeners();
		nicknameInput.onValueChanged.RemoveAllListeners();
	}

	private void EmitConfirm()
	{
		var name = nicknameInput.text;

		if (!TrySetNickname(name, out var error))
		{
			//popup.Show(error);
			// TODO: show error to user with popup
			return;
		}

		// Name valid => switch panels
		ShowLobbyPanel();
	}

	private void OnNameChanged(string text)
	{
		// Optional UX: enable confirm only if not empty
		confirmButton.interactable = !string.IsNullOrWhiteSpace(text);
	}

	private void LoadFromPrefs()
	{
		Nickname = PlayerPrefs.GetString(PrefKeys.PLAYER_NAME, "");
	}

	private void DecidePanels()
	{
		// If nickname already exists, go straight to lobby panel
		if (HasNickname)
		{
			ShowLobbyPanel();
			return;
		}

		ShowNamePanel();
	}

	private void ShowNamePanel()
	{
		namePanelRoot.SetActive(true);
		lobbyPanelRoot.SetActive(false);

		// Focus the input
		nicknameInput.Select();
	}

	private void ShowLobbyPanel()
	{
		namePanelRoot.SetActive(false);
		lobbyPanelRoot.SetActive(true);
	}

	private bool TrySetNickname(string name, out string error)
	{
		// Basic validation rules (customize as needed)
		if (string.IsNullOrWhiteSpace(name))
		{
			error = "Nickname cannot be empty.";
			return false;
		}
		if (name.Length < 3)
		{
			error = "Nickname must be at least 3 characters.";
			return false;
		}
		if (name.Length > 16)
		{
			error = "Nickname must be 16 characters or less.";
			return false;
		}

		Nickname = name.Trim();
		PlayerPrefs.SetString(PrefKeys.PLAYER_NAME, Nickname);
		error = null;
		return true;
	}
}
