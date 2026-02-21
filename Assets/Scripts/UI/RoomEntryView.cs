using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntryView : MonoBehaviour
{
	[SerializeField, FoldoutGroup("Main UI")] private TMP_Text roomNameText;
	[SerializeField, FoldoutGroup("Main UI")] private TMP_Text playerCountText;
	[SerializeField, FoldoutGroup("Main UI")] private GameObject lockIcon;
	[SerializeField, FoldoutGroup("Main UI")] private Button joinButton;

	[SerializeField, FoldoutGroup("Password UI (inside this prefab)")] private GameObject passwordPanel;          // inactive by default
	[SerializeField, FoldoutGroup("Password UI (inside this prefab)")] private TMP_InputField passwordInput;
	[SerializeField, FoldoutGroup("Password UI (inside this prefab)")] private Button acceptPasswordButton;

	[SerializeField, FoldoutGroup("Visual Feedback (optional)")] private Color flashColor = Color.red;
	[SerializeField, FoldoutGroup("Visual Feedback (optional)")] private float flashDuration = 0.6f;
	[SerializeField, FoldoutGroup("Visual Feedback (optional)")] private TMP_Text playerCountLabelToFlash;
	[SerializeField, FoldoutGroup("Visual Feedback (optional)")] private TMP_Text passwordTextToFlash;

	// Cached state
	private string roomName;
	private bool hasPassword;
	private int currentPlayers;
	private int maxPlayers;
	private bool isOpen;

	public event Action<string> JoinRequested;                 // roomName (no password)
	public event Action<string, string> PasswordSubmitted;     // roomName, password

	private void Awake()
	{
		joinButton.onClick.AddListener(OnJoinClicked);
		if (acceptPasswordButton != null)
			acceptPasswordButton.onClick.AddListener(OnAcceptPassword);
	}

	private void OnDestroy()
	{
		joinButton.onClick.RemoveAllListeners();
		if (acceptPasswordButton != null)
			acceptPasswordButton.onClick.RemoveAllListeners();
	}

	/// <summary>
	/// Populate visuals from VM
	/// </summary>
	public void Set(RoomViewModel vm)
	{
		roomName = vm.Name;
		hasPassword = vm.HasPassword;
		currentPlayers = vm.Current;
		maxPlayers = vm.Max;
		isOpen = vm.IsOpen;

		roomNameText.text = vm.Name;
		playerCountText.text = $"{vm.Current}/{vm.Max}";
		lockIcon.SetActive(vm.HasPassword);

		bool canJoin = isOpen && currentPlayers < maxPlayers;
		joinButton.interactable = canJoin;

		// Always hide the password panel on refresh
		if (passwordPanel != null)
		{
			passwordPanel.SetActive(false);
			if (passwordInput != null) passwordInput.text = string.Empty;
		}
	}

	private void OnJoinClicked()
	{
		// Room full or closed
		if (!isOpen || currentPlayers >= maxPlayers)
		{
			FlashPlayerCount();
			return;
		}

		// If room has a password, open the inline panel instead of raising an event
		if (hasPassword && passwordPanel != null)
		{
			joinButton.gameObject.SetActive(false);
			passwordPanel.SetActive(true);
			if (passwordInput != null)
				passwordInput.text = string.Empty;
			return;
		}

		// No password => bubble up to presenter
		JoinRequested?.Invoke(roomName);
	}

	private void OnAcceptPassword()
	{
		// Room might have become full by the time user typed the password
		if (!isOpen || currentPlayers >= maxPlayers)
		{
			ClosePasswordPanel();
			FlashPlayerCount();
			return;
		}

		var pass = passwordInput != null ? passwordInput.text : string.Empty;
		if (string.IsNullOrEmpty(pass))
		{
			FlashPasswordText();
			return;
		}

		PasswordSubmitted?.Invoke(roomName, pass);
		// Do not close immediately; presenter will refresh the list on success/fail and this will hide panel via Set()
	}

	private void ClosePasswordPanel()
	{
		if (passwordPanel != null) passwordPanel.SetActive(false);
		joinButton.gameObject.SetActive(true);
	}

	private void FlashPlayerCount()
	{
		if (playerCountLabelToFlash == null) return;
		StartCoroutine(FlashText(playerCountLabelToFlash));
	}

	private void FlashPasswordText()
	{
		if (passwordTextToFlash == null) return;
		StartCoroutine(FlashText(passwordTextToFlash));
	}

	private IEnumerator FlashText(TMP_Text t)
	{
		var original = t.color;
		t.color = flashColor;
		yield return new WaitForSeconds(flashDuration * 0.5f);
		t.color = original;
		yield return new WaitForSeconds(flashDuration * 0.5f);
	}
}