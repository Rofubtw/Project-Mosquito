using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
	public static class RichTextExt
	{
		public static string RichColored(this string message, Color color)
		{
			return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(color), message);
		}

		public static void DoFontSize(this Text text, int size, int increamentCount = 1)
		{
			text.StartCoroutine(DoFontSizeIE(text, size, increamentCount));
		}

		static IEnumerator DoFontSizeIE(Text text, int size, int increamentCount = 1)
		{
			WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

			while (text.fontSize != size)
			{
				text.fontSize += increamentCount;
				yield return waitForFixedUpdate;
			}
		}
	}
}