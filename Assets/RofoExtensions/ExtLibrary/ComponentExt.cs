using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
	public static class ComponentExt
	{
		public static bool HasBeenDestroyed(this Component self)
		{
			return self == null || self.gameObject == null;
		}

		public static T GetOrAddComponent<T>(this Component self) where T : Component
		{
			return self.GetComponent<T>() ?? self.gameObject.AddComponent<T>();
		}

		public static T AddComponent<T>(this Component self) where T : Component
		{
			return self.gameObject.AddComponent<T>();
		}

		public static GameObject[] GetAllChildren(this Transform self, bool includeInactive = false, bool returnAsList = true)
		{
			return self
				.GetComponentsInChildren<Transform>(includeInactive)
				.Where(c => c != self.transform)
				.Select(c => c.gameObject)
				.ToArray();
		}
		public static List<Transform> GetAllChildrenTransforms(this Transform self, bool includeInactive = false)
		{
			return self
				.GetComponentsInChildren<Transform>(includeInactive)
				.Where(c => c != self.transform)
				.Select(c => c.transform)
				.ToList();
		}
		public static List<GameObject> GetChildren(this Transform self)
		{
			List<GameObject> children = new List<GameObject>(self.transform.childCount);

			for (int i = 0; i < self.transform.childCount; i++)
				children[i] = self.transform.GetChild(i).gameObject;

			return children;
		}

		public static List<Transform> GetChildrenTransforms(this Transform self)
		{
			Transform[] children = new Transform[self.transform.childCount];

			for (int i = 0; i < self.transform.childCount; i++)
				children[i] = self.transform.GetChild(i);

			return children.ToList();
		}

		public static T[] GetComponentsInChildrenWithoutSelf<T>(this Transform self, bool includeInActive = true) where T : Component
		{
			return self.GetComponentsInChildren<T>(includeInActive)
				.Where(c => self.gameObject != c.gameObject)
				.ToArray()
			;
		}

		public static void RemoveComponent<T>(this Component self) where T : Component
		{
			GameObject.Destroy(self.GetComponent<T>());
		}

		public static void RemoveComponents<T>(this Component self) where T : Component
		{
			foreach (Component component in self.GetComponents<T>())
			{
				GameObject.Destroy(component);
			}
		}

		public static void RemoveComponentImmediate<T>(this Component self) where T : Component
		{
			GameObject.DestroyImmediate(self.GetComponent<T>());
		}

		public static void RemoveComponentsImmediate<T>(this Component self) where T : Component
		{
			foreach (Component component in self.GetComponents<T>())
			{
				GameObject.DestroyImmediate(component);
			}
		}
		public static void SetActiveChildren(this Transform self, bool isActive)
		{
			for (int i = 0; i < self.transform.childCount; i++)
				self.transform.GetChild(i).gameObject.SetActive(isActive);
		}
		public static void SetActiveChildren(this Transform aParent, int from, int to, bool isActive)
		{
			for (int i = from; i < to; i++)
				aParent.transform.GetChild(i).gameObject.SetActive(isActive);

			for (int j = to; j < aParent.transform.childCount; j++)
				aParent.transform.GetChild(j).gameObject.SetActive(!isActive);
		}

		public static void AddChild(this Component aParent, Component child)
		{
			child.transform.SetParent(aParent.transform);
		}
		public static void SetParentAndResetTransform(this Component myTransform, Component parent)
		{
			myTransform.transform.SetParent(parent.transform);
			myTransform.transform.Reset();
		}

		public static bool IsLastChild(this Component aParent)
		{
			if (aParent.transform.parent == null)
				return false;

			int childCount = aParent.transform.parent.childCount;
			int siblingIndex = aParent.transform.GetSiblingIndex();
			return childCount - 1 == siblingIndex;
		}

		public static void IsTrigger(this Collider aParent, bool isTrigger)
		{
			aParent.GetComponent<Collider>().isTrigger = isTrigger;
		}

		public static void IsTriggerAll(this Transform aParent, bool isTrigger, bool includeInactive)
		{
			var children = aParent.GetComponentsInChildren<Collider>(includeInactive);

			foreach (var item in children)
				item.isTrigger = isTrigger;
		}

		public static bool HasComponent<T>(this Component self) where T : Component
		{
			return self.GetComponent<T>() != null;
		}

		public static T CopyComponent<T>(T original, GameObject destination) where T : Component
		{
			System.Type type = original.GetType();
			var dst = destination.GetComponent(type) as T;
			if (!dst) dst = destination.AddComponent(type) as T;
			var fields = type.GetFields();

			foreach (var field in fields)
			{
				if (field.IsStatic) continue;

				field.SetValue(dst, field.GetValue(original));
			}
			var props = type.GetProperties();

			foreach (var prop in props)
			{
				if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name" || prop.Name == "hideFlags")
				{
					continue;
				}
				prop.SetValue(dst, prop.GetValue(original, null), null);
			}
			return dst as T;
		}

		public static T[] GetAllComponents<T>(this GameObject gameObject) where T : class
		{
			gameObject = gameObject.transform.root.gameObject;
			if (typeof(T).IsInterface)
			{
				List<T> ret = new List<T>();
				T add;
				foreach (var c in gameObject.GetComponentsInChildren<Component>())
				{
					add = c as T;
					if (add != null) ret.Add(add);
				}
				return ret.ToArray();
			}
			else return gameObject.GetComponentsInChildren<T>();
		}

		public static TYPE GetFirstComponent<TYPE>(this GameObject gameObject) where TYPE : class
		{
			gameObject = gameObject.transform.root.gameObject;
			if (typeof(TYPE).IsInterface)
			{
				foreach (var c in gameObject.GetComponentsInChildren<Component>())
				{
					if (c is TYPE) return c as TYPE;
				}
				return null;
			}
			else return gameObject.GetComponentInChildren<TYPE>();
		}

		public static TYPE GetOrAddComponent<TYPE>(this GameObject gameObject) where TYPE : Component
		{
			TYPE component = gameObject.GetComponent<TYPE>();
			if (component == null) component = gameObject.AddComponent<TYPE>();
			return component;
		}
	}
}