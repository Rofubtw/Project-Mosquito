using System;
using System.ComponentModel;
using Random = UnityEngine.Random;

namespace Extensions
{
	public static class EnumExt
	{
		public static T RandomEnumValue<T>()
		{
			var values = Enum.GetValues(typeof(T));
			int random = Random.Range(0, values.Length);
			return (T)values.GetValue(random);
		}

		public static TEnum ConvertEnum<TEnum>(this Enum source) => (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), true);

		public static string ToDescription(this Enum value)
		{
			DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
			return da.Length > 0 ? da[0].Description : value.ToString();
		}

		public static T Next<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

			T[] Arr = (T[])Enum.GetValues(src.GetType());
			int j = Array.IndexOf<T>(Arr, src) + 1;
			return (Arr.Length == j) ? Arr[0] : Arr[j];
		}

		public static TEnum GetLastValue<TEnum>()
		{
			if (!typeof(TEnum).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(TEnum).FullName));

			var values = Enum.GetValues(typeof(TEnum));
			var last = values.GetValue(values.Length - 1);
			return (TEnum)last;
		}

		public static int GetLenght<TEnum>()
		{
			if (!typeof(TEnum).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(TEnum).FullName));

			int count = Enum.GetValues(typeof(TEnum)).Length;
			return count;
		}
	}
}
