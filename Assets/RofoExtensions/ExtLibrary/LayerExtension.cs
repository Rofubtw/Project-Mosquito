using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extensions
{
	public static class LayerExtension
	{
		public static void SetLayer(this ref LayerMask layerMask, string layerName)
		{
			int mask = 0;
			mask |= (1 << LayerMask.NameToLayer(layerName));
			mask |= (1 << LayerMask.NameToLayer(layerName));
			layerMask = mask;
		}
		public static void IgnoreLayer(this ref LayerMask layerMask, string layerName)
		{
			int mask = 0;
			mask |= (1 << LayerMask.NameToLayer(layerName));
			mask |= (1 << LayerMask.NameToLayer(layerName));
			mask = ~mask;
			layerMask = mask;
		}
	}
}