using System;
using UnityEngine;

namespace Extensions
{
	public static class IntExt
	{
		public static void Times(this int self, Action act)
		{
			for (int i = 0; i < self; i++)
			{
				act();
			}
		}

		public static void Times(this int self, Action<int> act)
		{
			for (int i = 0; i < self; i++)
			{
				act(i);
			}
		}

		public static void TimesReverse(this int self, Action<int> act)
		{
			for (int i = self - 1; 0 <= i; i--)
			{
				act(i);
			}
		}

		public static string ZeroFill(this int self, int numberOfDigits)
		{
			return self.ToString("D" + numberOfDigits.ToString());
		}

		public static string FixedPoint(this int self, int numberOfDigits)
		{
			return self.ToString("F" + numberOfDigits.ToString());
		}

		public static int Repeat(this int self, int value, int max)
		{
			if (max == 0) return self;
			return (self + value + max) % max;
		}

		public static bool IsEven(this int self)
		{
			return self % 2 == 0;
		}

		public static bool IsOdd(this int self)
		{
			return self % 2 == 1;
		}

		public static float Clamp(this int value, int min, int max)
		{
			return Mathf.Clamp(value, min, max);
		}

		private const string COMMAS_FORMAT = "{0:n0}";
		private const string STR_K = "K";


		public static string ToNumberWithCommas(this int value)
		{
			return string.Format(COMMAS_FORMAT, value);
		}

		public static string ToNumberWithK(this int value)
		{
			if (value < 1000)
			{
				return value.ToString();
			}

			//return string.Concat(
			//    Mathf.FloorToInt(value / 1000).ToString(),
			//    STR_K);
			return string.Concat(
				(value / 1000f).ToString(), STR_K);
		}

		public static int[] GetDigits(int number)
		{
			string temp = number.ToString();
			int[] rtn = new int[temp.Length];
			for (int i = 0; i < rtn.Length; i++)
			{
				rtn[i] = int.Parse(temp[i].ToString());
			}
			return rtn;
		}
		public static bool IsPrime(int number)
		{
			if (number < 2)
				return false;
			if (number == 2)
				return true;
			if (number % 2 == 0)
				return false;

			for (int i = 3; i <= Mathf.Sqrt(number); i += 2)
			{
				if (number % i == 0)
					return false;
			}

			return true;
		}

	}
}
