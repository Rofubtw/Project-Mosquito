using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
	public static class ColorExt
	{
		public static class MyColors
		{
			public static Color GetRandomColor()
			{
				return colors.RandomAt();
			}

			public static Color32[] colors =
				{
			new Color32(240, 248, 255, 255),	//Color_AliceBlue
			new Color32(250, 235, 215, 255),	//Color_AntiqueWhite 
			new Color32(0, 255, 255, 255),		//Color_Aqua 
			new Color32(127, 255, 212, 255),	//Color_Aquamarine 
			new Color32(240, 255, 255, 255),	//Color_Azure 
			new Color32(245, 245, 220, 255),	//Color_Beige 
			new Color32(255, 228, 196, 255),	//Color_Bisque 
			new Color32(0, 0, 0, 255),			//Color_Black
			new Color32(255, 235, 205, 255),	//Color_BlanchedAlmond 
			new Color32(0, 0, 255, 255),		//Color_Blue 
			new Color32(138, 43, 226, 255),		//Color_BlueViolet
			new Color32(165, 42, 42, 255),		//Color_Brown
			new Color32(222, 184, 135, 255),	//Color_Burlywood 
			new Color32(95, 158, 160, 255),		//Color_CadetBlue 
			new Color32(127, 255, 0, 255),		//Color_Chartreuse 
			new Color32(210, 105, 30, 255),		//Color_Chocolate
			new Color32(255, 127, 80, 255),		//Color_Coral 
			new Color32(100, 149, 237, 255),	//Color_CornflowerBlue
			new Color32(255, 248, 220, 255),	//Color_Cornsilk 
			new Color32(220, 20, 60, 255),		//Color_Crimson 
			new Color32(0, 255, 255, 255),		//Color_Cyan
			new Color32(0, 0, 139, 255),		//Color_DarkBlue 
			new Color32(0, 139, 139, 255),		//Color_DarkCyan
			new Color32(184, 134, 11, 255),		//Color_DarkGoldenrod
			new Color32(169, 169, 169, 255),	//Color_DarkGray 
			new Color32(0, 100, 0, 255),		//Color_DarkGreen 
			new Color32(189, 183, 107, 255),	//Color_DarkKhaki
			new Color32(139, 0, 139, 255),		//Color_DarkMagenta 
			new Color32(85, 107, 47, 255),		//Color_DarkOliveGreen 
			new Color32(255, 140, 0, 255),		//Color_DarkOrange
			new Color32(153, 50, 204, 255),		//Color_DarkOrchid 
			new Color32(139, 0, 0, 255),		//Color_DarkRed 
			new Color32(233, 150, 122, 255),	//Color_DarkSalmon 
			new Color32(143, 188, 143, 255),	//Color_DarkSeaGreen 
			new Color32(72, 61, 139, 255),		//Color_DarkSlateBlue
			new Color32(47, 79, 79, 255),		//Color_DarkSlateGray 
			new Color32(0, 206, 209, 255),		//Color_DarkTurquoise 
			new Color32(148, 0, 211, 255),		//Color_DarkViolet
			new Color32(255, 20, 147, 255),		//Color_DeepPink  
			new Color32(0, 191, 255, 255),		//Color_DeepSkyBlue 
			new Color32(105, 105, 105, 255),	//Color_DimGray 
			new Color32(30, 144, 255, 255),		//Color_DodgerBlue 
			new Color32(178, 34, 34, 255),		//Color_FireBrick 
			new Color32(255, 250, 240, 255),	//Color_FloralWhite 
			new Color32(34, 139, 34, 255),		//Color_ForestGreen 
			new Color32(255, 0, 255, 255),		//Color_Fuchsia 
			new Color32(220, 220, 220, 255),	//Color_Gainsboro
			new Color32(248, 248, 255, 255),	//Color_GhostWhite 
			new Color32(255, 215, 0, 255),		//Color_Gold
			new Color32(218, 165, 32, 255),		//Color_Goldenrod 
			new Color32(128, 128, 128, 255),	//Color_Gray 
			new Color32(0, 128, 0, 255),		//Color_Green  
			new Color32(173, 255, 47, 255),		//Color_GreenYellow 
			new Color32(240, 255, 240, 255),	//Color_Honeydew 
			new Color32(255, 105, 180, 255),	//Color_HotPink 
			new Color32(205, 92, 92, 255),		//Color_IndianRed 
			new Color32(75, 0, 130, 255),		//Color_Indigo 
			new Color32(255, 255, 240, 255),	//Color_Ivory 
			new Color32(240, 230, 140, 255),	//Color_Khaki 
			new Color32(230, 230, 250, 255),	//Color_Lavender
			new Color32(255, 240, 245, 255),	//Color_Lavenderblush 
			new Color32(124, 252, 0, 255),		//Color_LawnGreen
			new Color32(255, 250, 205, 255),	//Color_LemonChiffon 
			new Color32(173, 216, 230, 255),	//Color_LightBlue 
			new Color32(240, 128, 128, 255),	//Color_LightCoral 
			new Color32(224, 255, 255, 255),	//Color_LightCyan 
			new Color32(250, 250, 210, 255),	//Color_LightGoldenodYellow 
			new Color32(211, 211, 211, 255),	//Color_LightGray
			new Color32(144, 238, 144, 255),	//Color_LightGreen 
			new Color32(255, 182, 193, 255),	//Color_LightPink 
			new Color32(255, 160, 122, 255),	//Color_LightSalmon 
			new Color32(32, 178, 170, 255),		//Color_LightSeaGreen 
			new Color32(135, 206, 250, 255),	//Color_LightSkyBlue 
			new Color32(119, 136, 153, 255),	//Color_LightSlateGray 
			new Color32(176, 196, 222, 255),	//Color_LightSteelBlue 
			new Color32(255, 255, 224, 255),	//Color_LightYellow 
			new Color32(0, 255, 0, 255),		//Color_Lime
			new Color32(50, 205, 50, 255),		//Color_LimeGreen 
			new Color32(250, 240, 230, 255),	//Color_Linen 
			new Color32(255, 0, 255, 255),		//Color_Magenta 
			new Color32(128, 0, 0, 255),		//Color_Maroon 
			new Color32(102, 205, 170, 255),	//Color_MediumAquamarine 
			new Color32(0, 0, 205, 255),		//Color_MediumBlue
			new Color32(186, 85, 211, 255),		//Color_MediumOrchid 
			new Color32(147, 112, 219, 255),	//Color_MediumPurple
			new Color32(60, 179, 113, 255),		//Color_MediumSeaGreen
			new Color32(123, 104, 238, 255),	//Color_MediumSlateBlue
			new Color32(0, 250, 154, 255),		//Color_MediumSpringGreen 
			new Color32(72, 209, 204, 255),		//Color_MediumTurquoise 
			new Color32(199, 21, 133, 255),		//Color_MediumVioletRed 
			new Color32(25, 25, 112, 255),		//Color_MidnightBlue 
			new Color32(245, 255, 250, 255),	//Color_Mintcream 
			new Color32(255, 228, 225, 255),	//Color_MistyRose 
			new Color32(255, 228, 181, 255),	//Color_Moccasin  
			new Color32(255, 222, 173, 255),	//Color_NavajoWhite 
			new Color32(0, 0, 128, 255),		//Color_Navy 
			new Color32(253, 245, 230, 255),	//Color_OldLace 
			new Color32(128, 128, 0, 255),		//Color_Olive 
			new Color32(107, 142, 35, 255),		//Color_Olivedrab 
			new Color32(255, 165, 0, 255),		//Color_Orange
			new Color32(255, 69, 0, 255),		//Color_Orangered 
			new Color32(218, 112, 214, 255),	//Color_Orchid
			new Color32(238, 232, 170, 255),	//Color_PaleGoldenrod 
			new Color32(152, 251, 152, 255),	//Color_PaleGreen
			new Color32(175, 238, 238, 255),	//Color_PaleTurquoise 
			new Color32(219, 112, 147, 255),	//Color_PaleVioletred 
			new Color32(255, 239, 213, 255),	//Color_PapayaWhip 
			new Color32(255, 218, 185, 255),	//Color_PeachPuff 
			new Color32(205, 133, 63, 255),		//Color_Peru 
			new Color32(255, 192, 203, 255),	//Color_Pink 
			new Color32(221, 160, 221, 255),	//Color_Plum 
			new Color32(176, 224, 230, 255),	//Color_PowderBlue
			new Color32(128, 0, 128, 255),		//Color_Purple 
			new Color32(255, 0, 0, 255),		//Color_Red 
			new Color32(188, 143, 143, 255),	//Color_RosyBrown 
			new Color32(65, 105, 225, 255),		//Color_RoyalBlue
			new Color32(139, 69, 19, 255),		//Color_SaddleBrown 
			new Color32(250, 128, 114, 255),	//Color_Salmon 
			new Color32(244, 164, 96, 255),		//Color_SandyBrown 
			new Color32(46, 139, 87, 255),		//Color_SeaGreen 
			new Color32(255, 245, 238, 255),	//Color_Seashell
			new Color32(160, 82, 45, 255),		//Color_Sienna 
			new Color32(192, 192, 192, 255),	//Color_Silver 
			new Color32(135, 206, 235, 255),	//Color_SkyBlue 
			new Color32(106, 90, 205, 255),		//Color_SlateBlue 
			new Color32(112, 128, 144, 255),	//Color_SlateGray
			new Color32(255, 250, 250, 255),	//Color_Snow 
			new Color32(0, 255, 127, 255),	    //Color_SpringGreen 
			new Color32(70, 130, 180, 255),		//Color_SteelBlue
			new Color32(210, 180, 140, 255),	//Color_Tan 
			new Color32(0, 128, 128, 255),		//Color_Teal
			new Color32(216, 191, 216, 255),	//Color_Thistle 
			new Color32(255, 99, 71, 255),		//Color_Tomato
			new Color32(64, 224, 208, 255),		//Color_Turquoise
			new Color32(238, 130, 238, 255),	//Color_Violet
			new Color32(245, 222, 179, 255),	//Color_Wheat
			new Color32(255, 255, 255, 255),	//Color_White 
			new Color32(245, 245, 245, 255),	//Color_WhiteSmoke 
			new Color32(255, 255, 0, 255),		//Color_Yellow 
			new Color32(154, 205, 50, 255),     //Color_YellowGreen
		};
		}

		#region Colors
		//public static readonly Color Color_AliceBlue = new Color32(240, 248, 255, 255);
		//public static readonly Color Color_AntiqueWhite = new Color32(250, 235, 215, 255);
		//public static readonly Color Color_Aqua = new Color32(0, 255, 255, 255);
		//public static readonly Color Color_Aquamarine = new Color32(127, 255, 212, 255);
		//public static readonly Color Color_Azure = new Color32(240, 255, 255, 255);
		//public static readonly Color Color_Beige = new Color32(245, 245, 220, 255);
		//public static readonly Color Color_Bisque = new Color32(255, 228, 196, 255);
		//public static readonly Color Color_Black = new Color32(0, 0, 0, 255);
		//public static readonly Color Color_BlanchedAlmond = new Color32(255, 235, 205, 255);
		//public static readonly Color Color_Blue = new Color32(0, 0, 255, 255);
		//public static readonly Color Color_BlueViolet = new Color32(138, 43, 226, 255);
		//public static readonly Color Color_Brown = new Color32(165, 42, 42, 255);
		//public static readonly Color Color_Burlywood = new Color32(222, 184, 135, 255);
		//public static readonly Color Color_CadetBlue = new Color32(95, 158, 160, 255);
		//public static readonly Color Color_Chartreuse = new Color32(127, 255, 0, 255);
		//public static readonly Color Color_Chocolate = new Color32(210, 105, 30, 255);
		//public static readonly Color Color_Coral = new Color32(255, 127, 80, 255);
		//public static readonly Color Color_CornflowerBlue = new Color32(100, 149, 237, 255);
		//public static readonly Color Color_Cornsilk = new Color32(255, 248, 220, 255);
		//public static readonly Color Color_Crimson = new Color32(220, 20, 60, 255);
		//public static readonly Color Color_Cyan = new Color32(0, 255, 255, 255);
		//public static readonly Color Color_DarkBlue = new Color32(0, 0, 139, 255);
		//public static readonly Color Color_DarkCyan = new Color32(0, 139, 139, 255);
		//public static readonly Color Color_DarkGoldenrod = new Color32(184, 134, 11, 255);
		//public static readonly Color Color_DarkGray = new Color32(169, 169, 169, 255);
		//public static readonly Color Color_DarkGreen = new Color32(0, 100, 0, 255);
		//public static readonly Color Color_DarkKhaki = new Color32(189, 183, 107, 255);
		//public static readonly Color Color_DarkMagenta = new Color32(139, 0, 139, 255);
		//public static readonly Color Color_DarkOliveGreen = new Color32(85, 107, 47, 255);
		//public static readonly Color Color_DarkOrange = new Color32(255, 140, 0, 255);
		//public static readonly Color Color_DarkOrchid = new Color32(153, 50, 204, 255);
		//public static readonly Color Color_DarkRed = new Color32(139, 0, 0, 255);
		//public static readonly Color Color_DarkSalmon = new Color32(233, 150, 122, 255);
		//public static readonly Color Color_DarkSeaGreen = new Color32(143, 188, 143, 255);
		//public static readonly Color Color_DarkSlateBlue = new Color32(72, 61, 139, 255);
		//public static readonly Color Color_DarkSlateGray = new Color32(47, 79, 79, 255);
		//public static readonly Color Color_DarkTurquoise = new Color32(0, 206, 209, 255);
		//public static readonly Color Color_DarkViolet = new Color32(148, 0, 211, 255);
		//public static readonly Color Color_DeepPink = new Color32(255, 20, 147, 255);
		//public static readonly Color Color_DeepSkyBlue = new Color32(0, 191, 255, 255);
		//public static readonly Color Color_DimGray = new Color32(105, 105, 105, 255);
		//public static readonly Color Color_DodgerBlue = new Color32(30, 144, 255, 255);
		//public static readonly Color Color_FireBrick = new Color32(178, 34, 34, 255);
		//public static readonly Color Color_FloralWhite = new Color32(255, 250, 240, 255);
		//public static readonly Color Color_ForestGreen = new Color32(34, 139, 34, 255);
		//public static readonly Color Color_Fuchsia = new Color32(255, 0, 255, 255);
		//public static readonly Color Color_Gainsboro = new Color32(220, 220, 220, 255);
		//public static readonly Color Color_GhostWhite = new Color32(248, 248, 255, 255);
		//public static readonly Color Color_Gold = new Color32(255, 215, 0, 255);
		//public static readonly Color Color_Goldenrod = new Color32(218, 165, 32, 255);
		//public static readonly Color Color_Gray = new Color32(128, 128, 128, 255);
		//public static readonly Color Color_Green = new Color32(0, 128, 0, 255);
		//public static readonly Color Color_GreenYellow = new Color32(173, 255, 47, 255);
		//public static readonly Color Color_Honeydew = new Color32(240, 255, 240, 255);
		//public static readonly Color Color_HotPink = new Color32(255, 105, 180, 255);
		//public static readonly Color Color_IndianRed = new Color32(205, 92, 92, 255);
		//public static readonly Color Color_Indigo = new Color32(75, 0, 130, 255);
		//public static readonly Color Color_Ivory = new Color32(255, 255, 240, 255);
		//public static readonly Color Color_Khaki = new Color32(240, 230, 140, 255);
		//public static readonly Color Color_Lavender = new Color32(230, 230, 250, 255);
		//public static readonly Color Color_Lavenderblush = new Color32(255, 240, 245, 255);
		//public static readonly Color Color_LawnGreen = new Color32(124, 252, 0, 255);
		//public static readonly Color Color_LemonChiffon = new Color32(255, 250, 205, 255);
		//public static readonly Color Color_LightBlue = new Color32(173, 216, 230, 255);
		//public static readonly Color Color_LightCoral = new Color32(240, 128, 128, 255);
		//public static readonly Color Color_LightCyan = new Color32(224, 255, 255, 255);
		//public static readonly Color Color_LightGoldenodYellow = new Color32(250, 250, 210, 255);
		//public static readonly Color Color_LightGray = new Color32(211, 211, 211, 255);
		//public static readonly Color Color_LightGreen = new Color32(144, 238, 144, 255);
		//public static readonly Color Color_LightPink = new Color32(255, 182, 193, 255);
		//public static readonly Color Color_LightSalmon = new Color32(255, 160, 122, 255);
		//public static readonly Color Color_LightSeaGreen = new Color32(32, 178, 170, 255);
		//public static readonly Color Color_LightSkyBlue = new Color32(135, 206, 250, 255);
		//public static readonly Color Color_LightSlateGray = new Color32(119, 136, 153, 255);
		//public static readonly Color Color_LightSteelBlue = new Color32(176, 196, 222, 255);
		//public static readonly Color Color_LightYellow = new Color32(255, 255, 224, 255);
		//public static readonly Color Color_Lime = new Color32(0, 255, 0, 255);
		//public static readonly Color Color_LimeGreen = new Color32(50, 205, 50, 255);
		//public static readonly Color Color_Linen = new Color32(250, 240, 230, 255);
		//public static readonly Color Color_Magenta = new Color32(255, 0, 255, 255);
		//public static readonly Color Color_Maroon = new Color32(128, 0, 0, 255);
		//public static readonly Color Color_MediumAquamarine = new Color32(102, 205, 170, 255);
		//public static readonly Color Color_MediumBlue = new Color32(0, 0, 205, 255);
		//public static readonly Color Color_MediumOrchid = new Color32(186, 85, 211, 255);
		//public static readonly Color Color_MediumPurple = new Color32(147, 112, 219, 255);
		//public static readonly Color Color_MediumSeaGreen = new Color32(60, 179, 113, 255);
		//public static readonly Color Color_MediumSlateBlue = new Color32(123, 104, 238, 255);
		//public static readonly Color Color_MediumSpringGreen = new Color32(0, 250, 154, 255);
		//public static readonly Color Color_MediumTurquoise = new Color32(72, 209, 204, 255);
		//public static readonly Color Color_MediumVioletRed = new Color32(199, 21, 133, 255);
		//public static readonly Color Color_MidnightBlue = new Color32(25, 25, 112, 255);
		//public static readonly Color Color_Mintcream = new Color32(245, 255, 250, 255);
		//public static readonly Color Color_MistyRose = new Color32(255, 228, 225, 255);
		//public static readonly Color Color_Moccasin = new Color32(255, 228, 181, 255);
		//public static readonly Color Color_NavajoWhite = new Color32(255, 222, 173, 255);
		//public static readonly Color Color_Navy = new Color32(0, 0, 128, 255);
		//public static readonly Color Color_OldLace = new Color32(253, 245, 230, 255);
		//public static readonly Color Color_Olive = new Color32(128, 128, 0, 255);
		//public static readonly Color Color_Olivedrab = new Color32(107, 142, 35, 255);
		//public static readonly Color Color_Orange = new Color32(255, 165, 0, 255);
		//public static readonly Color Color_Orangered = new Color32(255, 69, 0, 255);
		//public static readonly Color Color_Orchid = new Color32(218, 112, 214, 255);
		//public static readonly Color Color_PaleGoldenrod = new Color32(238, 232, 170, 255);
		//public static readonly Color Color_PaleGreen = new Color32(152, 251, 152, 255);
		//public static readonly Color Color_PaleTurquoise = new Color32(175, 238, 238, 255);
		//public static readonly Color Color_PaleVioletred = new Color32(219, 112, 147, 255);
		//public static readonly Color Color_PapayaWhip = new Color32(255, 239, 213, 255);
		//public static readonly Color Color_PeachPuff = new Color32(255, 218, 185, 255);
		//public static readonly Color Color_Peru = new Color32(205, 133, 63, 255);
		//public static readonly Color Color_Pink = new Color32(255, 192, 203, 255);
		//public static readonly Color Color_Plum = new Color32(221, 160, 221, 255);
		//public static readonly Color Color_PowderBlue = new Color32(176, 224, 230, 255);
		//public static readonly Color Color_Purple = new Color32(128, 0, 128, 255);
		//public static readonly Color Color_Red = new Color32(255, 0, 0, 255);
		//public static readonly Color Color_RosyBrown = new Color32(188, 143, 143, 255);
		//public static readonly Color Color_RoyalBlue = new Color32(65, 105, 225, 255);
		//public static readonly Color Color_SaddleBrown = new Color32(139, 69, 19, 255);
		//public static readonly Color Color_Salmon = new Color32(250, 128, 114, 255);
		//public static readonly Color Color_SandyBrown = new Color32(244, 164, 96, 255);
		//public static readonly Color Color_SeaGreen = new Color32(46, 139, 87, 255);
		//public static readonly Color Color_Seashell = new Color32(255, 245, 238, 255);
		//public static readonly Color Color_Sienna = new Color32(160, 82, 45, 255);
		//public static readonly Color Color_Silver = new Color32(192, 192, 192, 255);
		//public static readonly Color Color_SkyBlue = new Color32(135, 206, 235, 255);
		//public static readonly Color Color_SlateBlue = new Color32(106, 90, 205, 255);
		//public static readonly Color Color_SlateGray = new Color32(112, 128, 144, 255);
		//public static readonly Color Color_Snow = new Color32(255, 250, 250, 255);
		//public static readonly Color Color_SpringGreen = new Color32(0, 255, 127, 255);
		//public static readonly Color Color_SteelBlue = new Color32(70, 130, 180, 255);
		//public static readonly Color Color_Tan = new Color32(210, 180, 140, 255);
		//public static readonly Color Color_Teal = new Color32(0, 128, 128, 255);
		//public static readonly Color Color_Thistle = new Color32(216, 191, 216, 255);
		//public static readonly Color Color_Tomato = new Color32(255, 99, 71, 255);
		//public static readonly Color Color_Turquoise = new Color32(64, 224, 208, 255);
		//public static readonly Color Color_Violet = new Color32(238, 130, 238, 255);
		//public static readonly Color Color_Wheat = new Color32(245, 222, 179, 255);
		//public static readonly Color Color_White = new Color32(255, 255, 255, 255);
		//public static readonly Color Color_WhiteSmoke = new Color32(245, 245, 245, 255);
		//public static readonly Color Color_Yellow = new Color32(255, 255, 0, 255);
		//public static readonly Color Color_YellowGreen = new Color32(154, 205, 50, 255);
		#endregion

		public static Color SetColorAlpha(this Color col, float alpha) => new Color(col.r, col.g, col.b, alpha);

		public static void SetColorAlpha(this Graphic graphic, float alpha) => graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);

		public static bool SafeColorEquals(this Color self, Color color2, float threshold = 0.001f) => (self.r.SafeEquals(color2.r, threshold) && self.g.SafeEquals(color2.g, threshold) && self.b.SafeEquals(color2.b, threshold));

		public static void SetColor(this Graphic graphic, Color color) => graphic.color = color;

		public static Color MixColorCMY(Color color1, Color color2) => new Color((color1.r * color2.r), (color1.g * color2.g), (color1.b * color2.b));

		public static Color MixColorRGB(Color color1, Color color2) => new Color((color1.r + color2.r) / 2, (color1.g + color2.g) / 2, (color1.b + color2.b) / 2);

		#region HSVExtension

		public static float GetHueValue(this float value, float minRange, float maxRange, float spectrumMinValue = 0, float spectrumMaxValue = 1)
		=> value.Remap(minRange, maxRange, spectrumMinValue, spectrumMaxValue);

		public static Color GetHueColor(this float val, float minRange, float maxRange, float saturation = 1, float value = 1, Vector2 spectrumValues = default)
			=> Color.HSVToRGB(val.GetHueValue(minRange, maxRange, spectrumMinValue: spectrumValues.x, spectrumMaxValue: spectrumValues.y), saturation, value);

		public static Color ConvertHSV2Color(float H, float S, float V)
		=> Color.HSVToRGB(H, S, V);

		public static float[] GetHSVValue(this Color clr)
		{
			float[] hsvValues = new float[3];
			Color.RGBToHSV(clr, out hsvValues[0], out hsvValues[1], out hsvValues[2]);
			return hsvValues;
		}

		#endregion HSVExtension
	}
}