using Extensions;
using Fusion.Photon.Realtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class RegionSelector : MonoBehaviour
{
	[SerializeField, FoldoutGroup("UI")] private TMP_Dropdown dropdown;

	public NetRegion Current { get; private set; } = NetRegion.Auto;

	//public event Action<int> RegionChanged;

	private void Awake()
	{
		LoadFromPrefs();

		dropdown?.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnDestroy() => dropdown?.onValueChanged.RemoveAllListeners();

	private void OnValueChanged(int newValue) => ChangeRegion((NetRegion)newValue);

	private void LoadFromPrefs()
	{
		int regionValue = PlayerPrefs.GetInt(PrefKeys.REGION, 0);
		SetValue(regionValue);
		ChangeRegion((NetRegion)regionValue);
	}

	public void SetValue(int index)
	{
		if (dropdown != null)
			dropdown.value = index;
	}

	public void ChangeRegion(NetRegion region)
	{
		Current = region;

		// Loads PhotonAppSettings from Resources and updates FixedRegion
		var settings = Resources.Load<PhotonAppSettings>("PhotonAppSettings");

		if (settings == null)
		{
			Debug.LogWarning("[RegionService] PhotonAppSettings not found in Resources.");
			return;
		}

		settings.AppSettings.FixedRegion = region switch
		{
			NetRegion.Auto => "",
			NetRegion.Asia => "asia",
			NetRegion.EU => "eu",
			NetRegion.JP => "jp",
			NetRegion.KR => "kr",
			NetRegion.US => "us",
			_ => ""
		};

		PlayerPrefs.SetInt(PrefKeys.REGION, (int)region);

		Debug.Log($"[RegionService] Set region to {region}. FixedRegion is now '{settings.AppSettings.FixedRegion}'.");
	}

}

public enum NetRegion { Auto = 0, Asia = 1, EU = 2, JP = 3, KR = 4, US = 5 }
