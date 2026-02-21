using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Extensions
{
	public static class RandomExt
	{
		public static Color GetRandomColor()
		{
			return ColorExt.MyColors.GetRandomColor();
		}

		public static Vector3 GetRandomRotation()
		{
			Vector3 me = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
			return me;
		}

		public static Quaternion GetRandomQuaternion()
		{
			Quaternion me = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
			return me;
		}
	}
}
