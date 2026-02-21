using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Extensions
{
	public static class ArrayExt
	{
		private static Random m_random = new Random();

		public static void SetActiveElements(this UnityEngine.GameObject[] array, bool isActive)
		{
			for (int i = 0; i < array.Length; i++)
				array[i].SetActive(isActive);
		}

		public static void SetActiveElements(this UnityEngine.Component[] array, bool isActive)
		{
			for (int i = 0; i < array.Length; i++)
				array[i].gameObject.SetActive(isActive);
		}

		public static void ShiftRight<T>(this T[] array, int shiftCount = 1)
		{
			T tempElemet = array[array.Length - 1];

			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = tempElemet;
		}

		public static void ShiftLeft<T>(this T[] array, int shiftCount = 1)
		{
			T tempElemet = array[0];

			Array.Copy(array, 1, array, 0, array.Length - 1);
			array[array.Length - 1] = tempElemet;
		}

		public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array) => Array.AsReadOnly(array);

		public static void Clear(this Array array) => Array.Clear(array, 0, array.Length);
		public static void Clear(this Array array, int index) => Array.Clear(array, index, array.Length);

		public static void Clear(this Array array, int index, int length) => Array.Clear(array, index, length);

		public static bool Exists<T>(this T[] array, Predicate<T> match) => Array.Exists(array, match);

		public static T Find<T>(this T[] array, Predicate<T> match) => Array.Find(array, match);

		public static T[] FindAll<T>(this T[] array, Predicate<T> match) => Array.FindAll(array, match);

		public static int FindIndex<T>(this T[] array, Predicate<T> match) => Array.FindIndex(array, match);

		public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match) => Array.FindIndex(array, startIndex, match);

		public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match) => Array.FindIndex(array, startIndex, count, match);

		public static T FindLast<T>(this T[] array, Predicate<T> match) => Array.FindLast(array, match);

		public static int FindLastIndex<T>(this T[] array, Predicate<T> match) => Array.FindLastIndex(array, match);

		public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match) => Array.FindLastIndex(array, startIndex, match);
		public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match) => Array.FindLastIndex(array, startIndex, count, match);

		public static UnityEngine.GameObject[] FindNearestWithinRadius(this UnityEngine.GameObject[] myArray, UnityEngine.Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myArray.FindAll(a => (a.transform.position - origin.position).sqrMagnitude <= radius);
			else
				return myArray.FindAll(a =>
				UnityEngine.Vector2.Distance(new UnityEngine.Vector2(origin.position.x, origin.position.z),
				new UnityEngine.Vector2(a.transform.position.x, a.transform.position.z)) <= radius);
		}

		public static UnityEngine.Transform[] FindNearestWithinRadius(this UnityEngine.Transform[] myArray, UnityEngine.Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myArray.FindAll(a => (a.transform.position - origin.position).sqrMagnitude <= radius);
			else
				return myArray.FindAll(a =>
				UnityEngine.Vector2.Distance(new UnityEngine.Vector2(origin.position.x, origin.position.z),
				new UnityEngine.Vector2(a.transform.position.x, a.transform.position.z)) <= radius);
		}

		public static UnityEngine.Component[] FindNearestWithinRadius(this UnityEngine.Component[] myArray, UnityEngine.Transform origin, float radius, bool controlYPosition = false)
		{
			if (controlYPosition)
				return myArray.FindAll(a => (a.transform.position - origin.position).sqrMagnitude <= radius);
			else
				return myArray.FindAll(a =>
				UnityEngine.Vector2.Distance(new UnityEngine.Vector2(origin.position.x, origin.position.z),
				new UnityEngine.Vector2(a.transform.position.x, a.transform.position.z)) <= radius);
		}

		public static UnityEngine.Component[] FindNearestWithinRectangle(this UnityEngine.Component[] myList, UnityEngine.Transform origin, UnityEngine.Vector3 size)
		{
			UnityEngine.Bounds a = new UnityEngine.Bounds(origin.position, size);
			return myList.FindAll(x => a.Contains(x.transform.position));
		}

		public static UnityEngine.GameObject[] FindNearestWithinRectangle(this UnityEngine.GameObject[] myList, UnityEngine.Transform origin, UnityEngine.Vector3 size)
		{
			UnityEngine.Bounds a = new UnityEngine.Bounds(origin.position, size);
			return myList.FindAll(x => a.Contains(x.transform.position));
		}

		public static UnityEngine.Transform[] FindNearestWithinRectangle(this UnityEngine.Transform[] myList, UnityEngine.Transform origin, UnityEngine.Vector3 size)
		{
			UnityEngine.Bounds a = new UnityEngine.Bounds(origin.position, size);
			return myList.FindAll(x => a.Contains(x.transform.position));
		}

		public static void ForEach<T>(this T[] array, Action<T> action)
		{
			for (int i = 0; i < array.Length; i++)
				action(array[i]);
		}

		public static void ForEachWithIterator<T>(this T[] array, Action<T, int> action)
		{
			for (int i = 0; i < array.Length; i++)
				action(array[i], i);
		}

		public static int IndexOf<T>(this T[] array, T value) => Array.IndexOf(array, value);

		public static int IndexOf(this Array array, Object value) => Array.IndexOf(array, value);

		public static int IndexOf<T>(this T[] array, T value, int startIndex) => Array.IndexOf(array, value, startIndex);

		public static int IndexOf(this Array array, Object value, int startIndex) => Array.IndexOf(array, value, startIndex);

		public static int IndexOf<T>(this T[] array, T value, int startIndex, int count) => Array.IndexOf(array, value, startIndex, count);

		public static int IndexOf(this Array array, Object value, int startIndex, int count) => Array.IndexOf(array, value, startIndex, count);

		public static int LastIndexOf<T>(this T[] array, T value) => Array.LastIndexOf(array, value);

		public static int LastIndexOf(this Array array, Object value) => Array.LastIndexOf(array, value);

		public static int LastIndexOf<T>(this T[] array, T value, int startIndex) => Array.LastIndexOf(array, value, startIndex);

		public static int LastIndexOf(this Array array, Object value, int startIndex) => Array.LastIndexOf(array, value, startIndex);
		public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count) => Array.LastIndexOf(array, value, startIndex, count);

		public static int LastIndexOf(this Array array, Object value, int startIndex, int count) => Array.LastIndexOf(array, value, startIndex, count);

		public static void Reverse<T>(this T[] array) => Array.Reverse(array);

		public static void Reverse<T>(this T[] array, int index, int length) => Array.Reverse(array, index, length);

		public static bool TrueForAll<T>(this T[] array, Predicate<T> match) => Array.TrueForAll(array, match);

		public static T First<T>(this T[] array) => array[0];

		public static T Last<T>(this T[] array) => array[array.Length - 1];

		public static T Get<T>(this T[] myList, int index)
		{
			if (myList == null || myList.Length <= index)
			{
				Log.ErrorMessage("List is null or empty!", Colors.red);
				return default;
			}
			else return myList[index];
		}

		public static T[] GetComponentsInElements<T>(this UnityEngine.Transform[] self)
		{
			List<T> tempComponents = new List<T>();

			for (int i = 0; i < self.Length; i++)
			{
				if ((self[i].GetComponent<T>() as UnityEngine.Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents.ToArray();
		}

		public static T[] GetComponentsInElements<T>(this UnityEngine.Component[] self)
		{
			List<T> tempComponents = new List<T>();

			for (int i = 0; i < self.Length; i++)
			{
				if ((self[i].GetComponent<T>() as UnityEngine.Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents.ToArray();
		}

		public static T[] GetComponentsInElements<T>(this UnityEngine.GameObject[] self)
		{
			List<T> tempComponents = new List<T>();

			for (int i = 0; i < self.Length; i++)
			{
				if ((self[i].GetComponent<T>() as UnityEngine.Component) != null)
					tempComponents.Add(self[i].GetComponent<T>());
			}
			return tempComponents.ToArray();
		}



		public static T ElementAtRandom<T>(this T[] array) => array.IsNotNullOrEmpty() ? array[UnityEngine.Random.Range(0, array.Length)] : default(T);

		public static void Shuffle<T>(this T[] array)
		{
			T[] temp = array;

			int n = array.Length;

			while (1 < n)
			{
				n--;
				int k = m_random.Next(n + 1);
				var tmp = temp[k];
				temp[k] = temp[n];
				temp[n] = tmp;
			}
		}

		public static T[] Remove<T>(this T[] arr, T item)
		{
			List<T> tempList = arr.ToList();
			tempList.Remove(item);
			return tempList.ToArray();
		}

		public static void RemoveAll<T>(this T[] arr, params T[] item)
		{
			List<T> tempList = arr.ToList();
			tempList.RemoveAll(a => item.Contains(a));
			arr = tempList.ToArray();
		}

		public static void ClearNullSlots<T>(this T[] arr)
		{
			List<T> tempList = arr.ToList();
			tempList.RemoveAll(a => a == null);
			arr = tempList.ToArray();
		}

		public static void FindIndex<T>(this T[] array, Predicate<T> match, Action<int> act)
		{
			var index = Array.FindIndex(array, match);

			if (index == -1)
				return;

			act(index);
		}

		public static void Sort<T>(this T[] array) => Array.Sort(array);

		public static void Sort<T>(this T[] array, Comparison<T> comparison) => Array.Sort(array, comparison);

		public static void Sort<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector) where TResult : IComparable
			=> Array.Sort(array, (x, y) => selector(x).CompareTo(selector(y)));


		public static void SortDescending<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector) where TResult : IComparable
			=> Array.Sort(array, (x, y) => selector(y).CompareTo(selector(x)));

		public static void Sort<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector1, Func<TSource, TResult> selector2) where TResult : IComparable
		=>
			Array.Sort(array, (x, y) =>
		   {
			   var result = selector1(x).CompareTo(selector1(y));
			   return result != 0 ? result : selector2(x).CompareTo(selector2(y));
		   });

		public static bool IsEmpty<T>(this T[] self) => self.Length == 0;

		public static bool IsNull<T>(this T[] self) => self == null;
	}
}