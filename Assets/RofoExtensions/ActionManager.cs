using System;
using System.Reflection;
using UnityEngine;
using Extensions;

public static partial class ActionManager
{
	public static Action Example { get; set; }

	public static void ClearActionManagerData()
	{
		var info = typeof(ActionManager)
		.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

		info.ForEach(a => a.SetValue(a.Name, null));
	}
}
