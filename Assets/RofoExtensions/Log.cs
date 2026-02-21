using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{

	public enum Colors
	{
		original,
		aqua,
		black,
		blue,
		brown,
		cyan,
		darkblue,
		fuchsia,
		green,
		grey,
		lightblue,
		lime,
		magenta,
		maroon,
		navy,
		olive,
		orange,
		purple,
		red,
		silver,
		teal,
		white,
		yellow,
		LightGreen,
	}

	public enum Style
	{
		Normal,
		Bold,
		Italics
	}

	public enum LogType
	{
		Message,
		Warning,
		Error
	}

	public static class Log
	{
		#region Utils
		public static string StrColored(this string message, Colors color)
		{
			if (color == Colors.original)
				return message;

			return string.Format("<color={0}>{1}</color>", color.ToString(), message);
		}

		public static string StrStyled(this string message, Style style)
		{
			switch (style)
			{
				case Style.Bold:
					return string.Format("<b>{0}</b>", message);
				case Style.Italics:
					return string.Format("<i>{0}</i>", message);
				case Style.Normal:
					return message;
			}
			return "Style Error";
		}
		#endregion

		public static void Message<T>(T message, Colors color = Colors.original, Style style = Style.Normal, bool isEditorMode = true, LogType logType = LogType.Message)
		{

			string CSmessage = StrColored(message.ToString(), color);
			CSmessage = StrStyled(CSmessage, style);

			if (isEditorMode)

			{

#if UNITY_EDITOR
				switch (logType)
				{
					case LogType.Message:
						Debug.Log(CSmessage);
						break;
					case LogType.Warning:
						Debug.LogWarning(CSmessage);
						break;
					case LogType.Error:
						Debug.LogError(CSmessage);
						break;
					default:
						break;
				}
#endif
			}
		}

		////
		///

		public static void ErrorMessage<T>(T message, Colors color = Colors.red, Style style = Style.Normal, bool isEditorMode = true)
		{
			string CSmessage = StrColored(message.ToString(), color);
			CSmessage = StrStyled(CSmessage, style);

			if (isEditorMode)
			{
#if UNITY_EDITOR
				Debug.LogError(CSmessage);
#endif
			}
		}

		////
		///

		public static void WarningMessage<T>(T message, Colors color = Colors.yellow, Style style = Style.Normal, bool isEditorMode = true)
		{
			string CSmessage = StrColored(message.ToString(), color);
			CSmessage = StrStyled(CSmessage, style);

			if (isEditorMode)
			{
#if UNITY_EDITOR
				Debug.LogWarning(CSmessage);
#endif
			}

		}

	}
}

