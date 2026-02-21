using UnityEngine;

namespace Extensions
{
	public static class RectExt
	{
		public enum AnchorPresets
		{
			TopLeft,
			TopCenter,
			TopRight,

			MiddleLeft,
			MiddleCenter,
			MiddleRight,

			BottomLeft,
			BottonCenter,
			BottomRight,
			BottomStretch,

			VertStretchLeft,
			VertStretchRight,
			VertStretchCenter,

			HorStretchTop,
			HorStretchMiddle,
			HorStretchBottom,

			StretchAll
		}

		public static Rect Shift(this Rect self, Vector2 offset)
		{
			self.x += offset.x;
			self.y += offset.y;
			return self;
		}

		public static Rect Shift(this Rect self, float offsetX, float offsetY)
		{
			self.x += offsetX;
			self.y += offsetY;
			return self;
		}


		public static void SetAnchorPosY(this RectTransform source, float posY)
		{
			source.anchoredPosition = new Vector2(source.anchoredPosition.x, posY);
		}
		public static void SetLeft(this RectTransform rt, float left)
		{
			rt.offsetMin = new Vector2(left, rt.offsetMin.y);
		}

		public static void SetRight(this RectTransform rt, float right)
		{
			rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
		}

		public static void SetTop(this RectTransform rt, float top)
		{
			rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
		}

		public static void SetBottom(this RectTransform rt, float bottom)
		{
			rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
		}
		public static void SetAnchor(this RectTransform source, AnchorPresets allign)
		{
			switch (allign)
			{
				case (AnchorPresets.TopLeft):

					source.anchorMin = new Vector2(0, 1);
					source.anchorMax = new Vector2(0, 1);
					break;

				case (AnchorPresets.TopCenter):

					source.anchorMin = new Vector2(0.5f, 1);
					source.anchorMax = new Vector2(0.5f, 1);
					break;

				case (AnchorPresets.TopRight):

					source.anchorMin = new Vector2(1, 1);
					source.anchorMax = new Vector2(1, 1);
					break;


				case (AnchorPresets.MiddleLeft):

					source.anchorMin = new Vector2(0, 0.5f);
					source.anchorMax = new Vector2(0, 0.5f);
					break;

				case (AnchorPresets.MiddleCenter):

					source.anchorMin = new Vector2(0.5f, 0.5f);
					source.anchorMax = new Vector2(0.5f, 0.5f);
					break;

				case (AnchorPresets.MiddleRight):

					source.anchorMin = new Vector2(1, 0.5f);
					source.anchorMax = new Vector2(1, 0.5f);
					break;


				case (AnchorPresets.BottomLeft):

					source.anchorMin = new Vector2(0, 0);
					source.anchorMax = new Vector2(0, 0);
					break;

				case (AnchorPresets.BottonCenter):

					source.anchorMin = new Vector2(0.5f, 0);
					source.anchorMax = new Vector2(0.5f, 0);
					break;

				case (AnchorPresets.BottomRight):

					source.anchorMin = new Vector2(1, 0);
					source.anchorMax = new Vector2(1, 0);
					break;


				case (AnchorPresets.HorStretchTop):

					source.anchorMin = new Vector2(0, 1);
					source.anchorMax = new Vector2(1, 1);
					break;

				case (AnchorPresets.HorStretchMiddle):

					source.anchorMin = new Vector2(0, 0.5f);
					source.anchorMax = new Vector2(1, 0.5f);
					break;

				case (AnchorPresets.HorStretchBottom):

					source.anchorMin = new Vector2(0, 0);
					source.anchorMax = new Vector2(1, 0);
					break;


				case (AnchorPresets.VertStretchLeft):

					source.anchorMin = new Vector2(0, 0);
					source.anchorMax = new Vector2(0, 1);
					break;

				case (AnchorPresets.VertStretchCenter):

					source.anchorMin = new Vector2(0.5f, 0);
					source.anchorMax = new Vector2(0.5f, 1);
					break;

				case (AnchorPresets.VertStretchRight):

					source.anchorMin = new Vector2(1, 0);
					source.anchorMax = new Vector2(1, 1);
					break;


				case (AnchorPresets.StretchAll):

					source.anchorMin = new Vector2(0, 0);
					source.anchorMax = new Vector2(1, 1);
					break;

			}
		}

		public static AnchorMinMax GetAnchorMinMax(this RectTransform source)
		{
			return new AnchorMinMax(source.anchorMin, source.anchorMax);
		}
		public static void SetAnchorMinMax(this RectTransform source, AnchorMinMax anchorMinMax)
		{
			source.anchorMin = anchorMinMax.anchorMin;
			source.anchorMax = anchorMinMax.anchorMax;
		}
	}

	public class AnchorMinMax
	{
		public readonly Vector2 anchorMax;
		public readonly Vector2 anchorMin;

		public AnchorMinMax(Vector2 anchorMin, Vector2 anchorMax)
		{
			this.anchorMin = anchorMin;
			this.anchorMax = anchorMax;
		}
	}
}