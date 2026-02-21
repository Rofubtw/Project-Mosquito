using Sirenix.OdinInspector;
using UnityEngine;

public class MeshCenterPivoter : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;

	[Button]
	public void FindCenter()
	{
		GameObject go = new GameObject("CenterPivot");

		go.transform.position = meshRenderer.bounds.center;
	}
}
