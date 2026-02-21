using System;
using System.Reflection;

namespace Extensions
{
	public static class EventExt
	{
		public static void ResetAllEventsAndDelegates<T>(this T target) where T : class
		{
			if (target == null) return;

			var fields = target.GetType()
				.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var field in fields)
			{
				if (typeof(Delegate).IsAssignableFrom(field.FieldType))
				{
					field.SetValue(target, null);
				}
			}
		}
	}

}
