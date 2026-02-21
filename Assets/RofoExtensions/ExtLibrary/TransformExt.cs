using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
	public enum CompareType
	{
		And,
		Or
	}

	public enum FaceType
	{
		Up,
		Down,
		Left,
		Right,
		Forward,
		Back
	}

	public static class TransformExt
	{
		public static void SetPos2D(this Transform tr, Vector3 pos)
		{
			tr.position = new Vector3(pos.x, pos.y, tr.position.z);
		}
		public static void SetPos2D(this Transform tr, float xPos, float yPos)
		{
			tr.position = new Vector3(xPos, yPos, tr.position.z);
		}

		public static void SetLocalPos2D(this Transform tr, float xPos, float yPos)
		{
			tr.localPosition = new Vector3(xPos, yPos, tr.localPosition.z);
		}

		public static void SetLocalPos2D(this Transform tr, Vector2 pos)
		{
			tr.localPosition = new Vector3(pos.x, pos.y, tr.localPosition.z);
		}

		public static void SetLocalPosZ(this Transform tr, float posZ)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, posZ);
		}

		public static void SetLocalPosY(this Transform tr, float posY)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, posY, tr.localPosition.z);
		}

		public static void SetLocalPosX(this Transform tr, float posX)
		{
			tr.localPosition = new Vector3(posX, tr.localPosition.y, tr.localPosition.z);
		}

		public static void SetPosX(this Transform tr, float xPos)
		{
			tr.position = new Vector3(xPos, tr.position.y, tr.position.z);
		}
		public static void SetPosY(this Transform tr, float yPos)
		{
			tr.position = new Vector3(tr.position.x, yPos, tr.position.z);
		}
		public static void SetPosZ(this Transform tr, float zPos)
		{
			tr.position = new Vector3(tr.position.x, tr.position.y, zPos);
		}

		public static IEnumerable<Vector3> ToVector3(this IEnumerable<Transform> transforms) => transforms.Select(a => a.position);

		/// <summary>
		/// Set world angle
		/// </summary>
		/// <param name="tr"></param>
		/// <param name="angle"></param>
		public static void SetAngleZ(this Transform tr, float angle)
		{
			tr.eulerAngles = new Vector3(tr.eulerAngles.x, tr.eulerAngles.y, angle);
		}

		public static void SetAngleY(this Transform tr, float angle)
		{
			tr.eulerAngles = new Vector3(tr.eulerAngles.x, angle, tr.eulerAngles.z);
		}

		public static void SetAngleX(this Transform tr, float angle)
		{
			tr.eulerAngles = new Vector3(angle, tr.eulerAngles.x, tr.eulerAngles.z);
		}

		public static void SetScaleOneValue(this Transform tr, float value)
		{
			tr.localScale = new Vector3(value, value, tr.localScale.z);
		}
		public static void SetLocalAngleZ(this Transform tr, float angle)
		{
			tr.localEulerAngles = new Vector3(tr.localEulerAngles.x, tr.localEulerAngles.y, angle);
		}

		public static void SetLocalScale2D(this Transform tr, Vector2 scale)
		{
			tr.localScale = new Vector3(scale.x, scale.y, tr.localEulerAngles.z);
		}

		public static void SetLocalScale2D(this Transform tr, float xScale, float yScale)
		{
			tr.localScale = new Vector3(xScale, yScale, tr.localEulerAngles.z);
		}

		//Breadth-first search
		public static Transform FindDeepChild(this Transform aParent, string aName)
		{
			var result = aParent.Find(aName);
			if (result != null)
				return result;
			foreach (Transform child in aParent)
			{
				result = child.FindDeepChild(aName);
				if (result != null)
					return result;
			}
			return null;
		}

		public static void Reset(this Transform aParent)
		{
			aParent.localPosition = Vector3.zero;
			aParent.localEulerAngles = Vector3.zero;
			aParent.localScale = Vector3.one;
		}

		public static Transform GetFirstChild(this Transform aParent)
		{
			return aParent.GetChild(0);
		}

		public static Transform GetLastChild(this Transform aParent)
		{
			if (aParent.childCount > 0)
				return aParent.GetChild(aParent.childCount - 1);
			else
				return null;
		}

		public static Transform GetRandomChild(this Transform aParent)
		{
			if (aParent.childCount > 0)
				return aParent.GetChild(Random.Range(0, aParent.childCount));
			else
				return null;
		}

		public static Vector3 GetFacePosition(this Transform aParent, FaceType faceType)
		{

			Vector3 halfScale = aParent.transform.localScale / 2f;
			Vector3 pos = Vector3.zero;
			switch (faceType)
			{
				case FaceType.Up:
					pos = aParent.transform.position + halfScale.y * Vector3.up;
					break;
				case FaceType.Down:
					pos = aParent.transform.position + halfScale.y * Vector3.down;
					break;
				case FaceType.Left:
					pos = aParent.transform.position + halfScale.x * Vector3.left;
					break;
				case FaceType.Right:
					pos = aParent.transform.position + halfScale.x * Vector3.right;
					break;
				case FaceType.Forward:
					pos = aParent.transform.position + halfScale.z * Vector3.forward;
					break;
				case FaceType.Back:
					pos = aParent.transform.position + halfScale.z * Vector3.back;
					break;
				default:
					break;
			}

			return pos;
		}

		public static bool CompareTags(this Transform aParent, CompareType type, params string[] tags)
		{
			bool result = false;

			switch (type)
			{
				case CompareType.And:
					result = true;
					for (int i = 0; i < tags.Length; i++)
					{
						result = result && aParent.CompareTag(tags[i]);
					}
					break;
				case CompareType.Or:
					result = false;
					for (int i = 0; i < tags.Length; i++)
					{
						result = result || aParent.CompareTag(tags[i]);
					}
					break;
				default:
					break;
			}

			return result;
		}

		public static Vector3 GetInspectorAngle(this Transform aParent)
		{
			Vector3 angle = aParent.eulerAngles;
			float x = angle.x;
			float y = angle.y;
			float z = angle.z;

			if (Vector3.Dot(aParent.up, Vector3.up) >= 0f)
			{
				if (angle.x >= 0f && angle.x <= 90f)
				{
					x = angle.x;
				}
				if (angle.x >= 270f && angle.x <= 360f)
				{
					x = angle.x - 360f;
				}
			}
			if (Vector3.Dot(aParent.up, Vector3.up) < 0f)
			{
				if (angle.x >= 0f && angle.x <= 90f)
				{
					x = 180 - angle.x;
				}
				if (angle.x >= 270f && angle.x <= 360f)
				{
					x = 180 - angle.x;
				}
			}

			if (angle.y > 180)
			{
				y = angle.y - 360f;
			}

			if (angle.z > 180)
			{
				z = angle.z - 360f;
			}

			return new Vector3(Mathf.Round(x), Mathf.Round(y), Mathf.Round(z));
		}

		public static Vector3 GetCenter(this Transform aParent)
		{
			var rends = aParent.GetComponentsInChildren<Renderer>();
			if (rends.Length == 0)
				return aParent.position;
			var b = rends[0].bounds;
			for (int i = 1; i < rends.Length; i++)
			{
				b.Encapsulate(rends[i].bounds);
			}
			return b.center;
		}

		public static void CenterOnChildren(this Transform aParent)
		{
			Vector3 defaultPos = aParent.position;

			Vector3 center = aParent.GetCenter();

			List<Transform> children = aParent.GetChildrenTransforms();

			children.ForEach(a => a.transform.parent = null);

			aParent.position = center;

			children.ForEach(a => a.transform.parent = aParent);

			aParent.position = defaultPos;
		}


		public static Vector3 GetMiddlePoint(this Transform first, Transform last) => (first.position + last.position) / 2f;

		/// <summary>
		/// It will set the object position to calculated position, also it will return the calculated position.
		/// </summary>
		/// <param name="objectTR"></param>
		/// <param name="uıCamera"></param>
		/// <param name="canvasTransform"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Vector3 Set3DObjectToUI(this Transform objectTR, Camera uıCamera, Transform canvasTransform, Vector3 scale)
		{
			Camera _mainCamera = Camera.main;

			Vector3 screenPos = _mainCamera.WorldToScreenPoint(objectTR.position);
			screenPos.z = _mainCamera.nearClipPlane;
			Vector3 pos;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(
			canvasTransform as RectTransform, screenPos,
			uıCamera,
			out pos);
			objectTR.SetParent(canvasTransform);
			objectTR.gameObject.SetLayer("UI");

			objectTR.position = pos;
			objectTR.localScale = scale;

			return pos;
		}

		public static Vector3 Calculate3DObjectToUIPos(this Vector3 objectPos, Camera uıCamera, Transform canvasTransform, Vector3 scale)
		{
			Camera _mainCamera = Camera.main;

			Vector3 screenPos = _mainCamera.WorldToScreenPoint(objectPos);
			screenPos.z = _mainCamera.nearClipPlane;
			Vector3 pos;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(
			canvasTransform as RectTransform, screenPos,
			uıCamera,
			out pos);

			return pos;
		}

		public static void SetScale(this Transform objectTR, Vector3 scale, bool keepChildScale = true)
		{
			if (keepChildScale)
			{
				List<Vector3> lossyScaleList = new List<Vector3>();
				List<Transform> children = objectTR.transform.GetChildrenTransforms();

				objectTR.transform.GetChildrenTransforms().ForEach(child => lossyScaleList.Add(child.lossyScale));
				objectTR.transform.localScale = scale;


				for (int i = 0; i < children.Count; i++)
				{
					children[i].localScale = new Vector3(lossyScaleList[i].x / objectTR.transform.localScale.x, lossyScaleList[i].y / objectTR.transform.localScale.y, lossyScaleList[i].z / objectTR.transform.localScale.z);
				}
			}

			else
				objectTR.transform.localScale = scale;
		}

		public static Vector3 UIToWorldPoint(this Transform uiObject, Camera orthoCam, Camera worldCam)
		{
			Vector3 screenPos = orthoCam.WorldToScreenPoint(uiObject.position);
			Vector3 worldPos = worldCam.ScreenToWorldPoint(screenPos);

			return worldPos;
		}


		public static void SetScale(this Transform objectTR, float scale, bool effectChildrenScale = false)
		{
			if (!effectChildrenScale)
			{
				List<Vector3> lossyScaleList = new List<Vector3>();

				List<Transform> children = objectTR.transform.GetChildrenTransforms();

				objectTR.transform.GetChildrenTransforms().ForEach(child => lossyScaleList.Add(child.lossyScale));
				objectTR.transform.localScale *= scale;


				for (int i = 0; i < children.Count; i++)
				{
					children[i].localScale = new Vector3(lossyScaleList[i].x / objectTR.transform.localScale.x, lossyScaleList[i].y / objectTR.transform.localScale.y, lossyScaleList[i].z / objectTR.transform.localScale.z);
				}
			}

			else
				objectTR.transform.localScale *= scale;
		}
	}
}
