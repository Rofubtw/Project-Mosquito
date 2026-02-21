using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private TextMeshProUGUI content;

	public void ShowPopup(string text)
	{
		content.text = text;
		gameObject.SetActive(true);
	}

	public void DisablePopup()
	{
		//var fusionManager = FusionManager.Instance;
		//if (fusionManager != null)
		//{
		//	if (fusionManager.Runner)
		//		fusionManager.Runner.Shutdown();
		//	Destroy(fusionManager);
		//}

		Debug.Log("Loading Menu scene...");
		SceneManager.LoadScene("Menu");
	}
}
