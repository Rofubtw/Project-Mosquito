using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{

	public static class ListExt
	{
		private static System.Random m_random = new System.Random();

		public static void Add<T>(this List<T> self, IEnumerable<T> collection) => self.AddRange(collection);

		public static void AddRange<T>(this List<T> list, params T[] collection) => list.AddRange(collection);

		public static void AddRange<T>(this List<T> list, params IList<T>[] collectionList)
		{
			for (int i = 0; i < collectionList.Length; i++)
				list.AddRange(collectionList[i]);
		}

		public static void Set<T>(this List<T> list, IEnumerable<T> collection)
		{
			list.Clear();
			list.AddRange(collection);
		}

		public static void SetActiveElements(this List<UnityEngine.GameObject> list, bool isActive)
		{
			if (list != null && list.Count > 0)
				for (int i = 0; i < list.Count; i++)
					list[i].SetActive(isActive);
		}

		public static void Set<T>(this List<T> list, params T[] collection)
		{
			list.Clear();
			list.AddRange(collection);
		}

		public static void Sort<T>(this List<T> self, Comparison<T> comparison) => self.Sort(comparison);

		public static List<GameObject> FindNearestWithinRadius(this List<GameObject> myList, Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myList.FindAll(a => (a.transform.position - origin.position).sqrMagnitude <= radius);
			else
				return myList.FindAll(a => Vector2.Distance(new Vector2(origin.position.x, origin.position.z), new Vector2(a.transform.position.x, a.transform.position.z)) <= radius);
		}

		public static List<Transform> FindNearestWithinRadius(this List<Transform> myList, Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myList.FindAll(a => (a.position - origin.position).sqrMagnitude <= radius);
			else
				return myList.FindAll(a => Vector2.Distance(new Vector2(origin.position.x, origin.position.z), new Vector2(a.position.x, a.position.z)) <= radius);
		}

		public static List<Component> FindNearestWithinRadius(this List<Component> myList, Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myList.FindAll(a => (a.transform.position - origin.position).sqrMagnitude <= radius);
			else
				return myList.FindAll(a => Vector2.Distance(new Vector2(origin.position.x, origin.position.z), new Vector2(a.transform.position.x, a.transform.position.z)) <= radius);
		}

		public static void Sort<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector) where TResult : IComparable
		=> self.Sort((x, y) => selector(x).CompareTo(selector(y)));

		public static void SortDescending<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector) where TResult : IComparable
		=> self.Sort((x, y) => selector(y).CompareTo(selector(x)));

		public static void Sort<TSource, TResult1, TResult2>(this List<TSource> self, Func<TSource, TResult1> selector1, Func<TSource, TResult2> selector2) where TResult1 : IComparable where TResult2 : IComparable
		=> self.Sort((x, y) =>
			{
				var result = selector1(x).CompareTo(selector1(y));
				return result != 0 ? result : selector2(x).CompareTo(selector2(y));
			});

		public static void SortDescending<TSource, TResult1, TResult2>(this List<TSource> self, Func<TSource, TResult1> selector1, Func<TSource, TResult2> selector2) where TResult1 : IComparable where TResult2 : IComparable
		=> self.Sort((x, y) =>
		{
			var result = selector1(y).CompareTo(selector1(x));
			return result != 0 ? result : selector2(x).CompareTo(selector2(y));
		});

		public static void Shuffle<T>(this List<T> selfList)
		{
			var count = selfList.Count;

			for (var i = 0; i < count - 1; ++i)
			{
				var r = UnityEngine.Random.Range(i, count);
				var tmp = selfList[i];
				selfList[i] = selfList[r];
				selfList[r] = tmp;
			}
		}

		public static void ShufflePlus<T>(this List<T> selfList)
		{
			var count = selfList.Count;

			List<T> baseList = new List<T>();
			baseList.AddRange(selfList);

			for (var i = 0; i < count - 1; ++i)
			{
				var r = UnityEngine.Random.Range(i, count);
				var tmp = selfList[i];
				selfList[i] = selfList[r];
				selfList[r] = tmp;
			}

			for (int i = 0; i < selfList.Count; i++)
			{
				if (selfList[i].Equals(baseList[i]))
				{
					selfList.Clear();
					selfList.AddRange(baseList);
					selfList.ShufflePlus();
				}
			}
		}

		public static void Remove<T>(this List<T> self, Predicate<T> match)
		{
			var index = self.FindIndex(match);
			if (index == -1) return;
			self.RemoveAt(index);
		}
		public static T RemoveLast<T>(this List<T> self)
		{
			if (self != null && self.Count != 0)
			{
				T t = self.GetLast();
				self.RemoveAt(self.Count - 1);
				return t;
			}

			return default;
		}

		public static T RemoveFirst<T>(this List<T> self)
		{
			if (self != null && self.Count != 0)
			{
				T t = self.GetFirst();
				self.RemoveAt(0);
				return t;
			}

			return default;
		}


		public static void InsertFirst<T>(this List<T> self, T item) => self.Insert(0, item);

		public static void RemoveSince<T>(this List<T> self, int count)
		{
			while (count <= self.Count)
				self.RemoveAt(self.Count - 1);
		}

		public static T GetLast<T>(this List<T> myList)
		{
			if (myList == null || myList.Count == 0)
			{
				Log.ErrorMessage("List is null or empty!", Colors.red);
				return default;
			}

			else return myList[myList.Count - 1];
		}
		public static T GetFirst<T>(this List<T> myList)
		{
			if (myList == null || myList.Count == 0)
			{
				Log.ErrorMessage("List is null or empty!", Colors.red);
				return default;
			}

			else return myList[0];
		}

		public static T Get<T>(this List<T> myList, int index)
		{
			if (myList == null || myList.Count <= index)
			{
				Log.ErrorMessage("List is null or empty!", Colors.red);
				return default;
			}
			else return myList[index];
		}


		public static void Fill<T>(this List<T> self, int startIndex, int endIndex, T value)
		{
			for (int i = startIndex; i < endIndex; i++)
				self.Add(value);
		}

		public static void SetSize<T>(this List<T> self, int size)
		{
			if (self.Count <= size) return;
			self.RemoveRange(size, self.Count - size);
		}


		public static List<GameObject> SortByDistance(this List<GameObject> objects, Vector3 mesureFrom) => objects.OrderBy(x => Vector3.Distance(x.transform.position, mesureFrom)).ToList();

		public static List<GameObject> RemoveNull(this List<GameObject> objects) => objects.Where(x => x != null).ToList();

		public static List<GameObject> SortByXValue(this List<GameObject> objects) => objects.OrderBy(v => v.transform.position.x).ToList();
		public static List<GameObject> SortByYValue(this List<GameObject> objects) => objects.OrderBy(v => v.transform.position.y).ToList();
		public static List<GameObject> SortByZValue(this List<GameObject> objects) => objects.OrderBy(v => v.transform.position.z).ToList();

		public static GameObject GetLast(this List<GameObject> objects) => objects[objects.Count - 1];

		public static bool IsEmpty<T>(this List<T> self) => self.Count == 0;

		public static bool IsNull<T>(this List<T> self) => self == null;

		public static void AddIfNotExist<T>(this List<T> self, T val)
		{
			if (!self.Contains(val))
				self.Add(val);
		}

		public static void ClearEmptySlots<T>(this List<T> self) => self.RemoveAll(a => a == null);


		public static List<T> GetComponentsInElements<T>(this List<Transform> self)
		{
			List<T> tempComponents = new List<T>();

			for (int i = 0; i < self.Count; i++)
			{
				if ((self[i].GetComponent<T>() as Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents;
		}

		public static List<T> GetComponentsInElements<T>(this List<Component> self)
		{
			List<T> tempComponents = new List<T>();

			for (int i = 0; i < self.Count; i++)
			{
				if ((self[i].GetComponent<T>() as Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents;
		}

		public static List<T> GetComponentsInElements<T>(this List<GameObject> self)
		{
			List<T> tempComponents = new List<T>();
			for (int i = 0; i < self.Count; i++)
			{
				if ((self[i].GetComponent<T>() as Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents;
		}

	}
}