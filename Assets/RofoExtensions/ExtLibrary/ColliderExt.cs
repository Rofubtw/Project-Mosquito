using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
	internal static class ColliderExt
	{
		public static Collider GetClosestCollider<T>(this IEnumerable<T> colliders, Vector3 originPosition)
		{
			IEnumerable<Collider> colls = colliders.Cast<Collider>().OrderBy(value => (value.transform.position - originPosition).sqrMagnitude);
			return colliders.Cast<Collider>().OrderBy(value => (value.transform.position - originPosition).sqrMagnitude).First();
		}

		public static Collider GetNClosestCollider<T>(this IEnumerable<T> colliders, Vector3 originPosition, int n)
		{
			IEnumerable<Collider> colls = colliders.Cast<Collider>().OrderBy(value => (value.transform.position - originPosition).sqrMagnitude);
			return n >= colls.Count() ? colls.First() : colls.ElementAt(n);
		}

		public static Collider GetFurthestCollider<T>(this IEnumerable<T> colliders, Vector3 originPosition)
		{
			return colliders.Cast<Collider>().OrderByDescending(value => (value.transform.position - originPosition).sqrMagnitude).First();
		}

		public static Collider GetNFurthestCollider<T>(this IEnumerable<T> colliders, Vector3 originPosition, int n)
		{
			IEnumerable<Collider> colls = colliders.Cast<Collider>().OrderByDescending(value => (value.transform.position - originPosition).sqrMagnitude);
			return n >= colls.Count() ? colls.First() : colls.ElementAt(n);
		}

		public static Collider[] FilterCollider(this Collider[] colliders, string filterTag)
		{
			List<Collider> filterColliders = new List<Collider>();

			foreach (Collider collider in colliders)
			{
				if (collider.CompareTag(filterTag))
					filterColliders.Add(collider);
			}

			return filterColliders.ToArray();
		}

		public static void DisableAllColliders(this Collider[] colliders)
		{
			foreach (var c in colliders)
			{
				c.enabled = false;
			}
		}

		public static void EnableAllColliders(this Collider[] colliders)
		{
			foreach (var c in colliders)
			{
				c.enabled = true;
			}
		}

		public static void SetAllCollidersTrigger(this Collider[] colliders, bool isTrigger)
		{
			foreach (var c in colliders)
			{
				c.isTrigger = isTrigger;
			}
		}

	}
}
