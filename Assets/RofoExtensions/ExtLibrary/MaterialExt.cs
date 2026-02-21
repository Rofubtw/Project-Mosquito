using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
	public enum BlendMode
	{
		Opaque,
		Cutout,
		Fade,
		Transparent
	}
	[System.Serializable]
	public class MaterialPropertyInfo
	{
		public Color mainColor = default;
		public Color emissionColor = default;
		public Texture2D albedoTexture = null;
		public float smoothness = -1;
		public float metallic = -1;
		public int materialIndex = 0;

		public MaterialPropertyInfo(
			Color mainColor = default,
			Color emissionColor = default,
			Texture2D albedoTexture = null,
			float smoothness = -1,
			float metallic = -1,
			int materialIndex = 0
			)
		{
			this.mainColor = mainColor;
			this.emissionColor = emissionColor;
			this.albedoTexture = albedoTexture;
			this.smoothness = smoothness;
			this.metallic = metallic;
			this.materialIndex = materialIndex;
		}
	}

	public static class MaterialExt
	{
		public static Material[] SetMaterial(this Material[] materials, int index, Material theNewMaterial)
		{
			var mats = materials;
			mats[index] = theNewMaterial;
			return mats;
		}

		public static Material SetupMaterialWithBlendMode(this Material material, BlendMode blendMode)
		{
			switch (blendMode)
			{
				case BlendMode.Opaque:
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					material.SetInt("_ZWrite", 1);
					material.DisableKeyword("_ALPHATEST_ON");
					material.EnableKeyword("_EMISSION");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = -1;
					break;
				case BlendMode.Cutout:
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					material.SetInt("_ZWrite", 1);
					material.EnableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_EMISSION");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = 2450;
					break;
				case BlendMode.Fade:
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					material.SetInt("_ZWrite", 0);
					material.DisableKeyword("_ALPHATEST_ON");
					material.EnableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_EMISSION");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = 3000;
					break;
				case BlendMode.Transparent:
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					material.SetInt("_ZWrite", 0);
					material.DisableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_EMISSION");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = 3000;
					break;
			}
			return material;
		}

		public static Material[] SetupMaterialWithBlendMode(this Material[] materials, BlendMode blendMode)
		{
			foreach (var mat in materials)
				mat.SetupMaterialWithBlendMode(blendMode);

			return materials;
		}

		public static Color EmissionColor(this Material mat) => mat.GetColor(RofoUtil.PROP_EMISSION_COLOR);

		public static Color EmissionColor(this MaterialPropertyBlock block) => block.GetColor(RofoUtil.PROP_EMISSION_COLOR);

		public static Color Color(this MaterialPropertyBlock block) => block.GetColor(RofoUtil.PROP_COLOR);

		public static void FadeMaterials(this Renderer rend, float fadeValue, float time, Ease ease, Action doOnComplete = null)
		{
			var materials = rend.materials;

			materials.ForEach(mat =>
			{
				mat = mat.SetupMaterialWithBlendMode(BlendMode.Fade);
				mat.DOFade(fadeValue, time).SetEase(ease).OnComplete(() =>
				{
					doOnComplete?.Invoke();
				});
			});

			rend.materials = materials;
		}

		public static void FadeMaterials(this Renderer[] rends, float fadeValue, float time, Ease ease, Action doOnComplete = null)
		{
			bool canWork = true;

			rends.ForEach(rend =>
			{
				var materials = rend.materials;

				materials.ForEach(mat =>
				{
					mat = mat.SetupMaterialWithBlendMode(BlendMode.Fade);
					mat.DOFade(fadeValue, time).SetEase(ease).OnComplete(() =>
					{
						if (canWork)
							doOnComplete?.Invoke();

						canWork = false;
					});
				});

				rend.materials = materials;
			});
		}

		public static void FadeMaterials(this List<Renderer> rends, float fadeValue, float time, Ease ease, Action doOnComplete = null)
		{
			bool canWork = true;

			rends.ForEach(rend =>
			{
				var materials = rend.materials;

				materials.ForEach(mat =>
				{
					mat = mat.SetupMaterialWithBlendMode(BlendMode.Fade);
					mat.DOFade(fadeValue, time).SetEase(ease).OnComplete(() =>
					{
						if (canWork)
							doOnComplete?.Invoke();

						canWork = false;
					});
				});

				rend.materials = materials;
			});
		}

		public static void FadeMaterials(this Material[] materials, float fadeValue, float time, Ease ease, Action doOnComplete)
		{
			materials.ForEach(mat =>
			{
				mat = mat.SetupMaterialWithBlendMode(BlendMode.Fade);
				mat.DOFade(fadeValue, time).SetEase(ease).OnComplete(() => doOnComplete?.Invoke());
			});
		}

		public static void SetSmoothness(this Material mat, float value) => mat.SetFloat(RofoUtil.PROP_SMOOTHNESS, value);

		public static void SetMetallic(this Material mat, float value) => mat.SetFloat(RofoUtil.PROP_METALLIC, value);

		public static void SetEmissionColor(this Material mat, Color value) => mat.SetColor(RofoUtil.PROP_EMISSION_COLOR, value);

		public static float Metallic(this Material mat) => mat.GetFloat(RofoUtil.PROP_METALLIC);

		public static float Smoothness(this Material mat) => mat.GetFloat(RofoUtil.PROP_SMOOTHNESS);

		public static void DoMetallicValue(this Material mat, float value, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			DOTween.To(() => mat.Metallic(), x => mat.SetMetallic(x), value, time)
		   .SetEase(ease)
		   .SetUpdate(updateType)
		   .OnComplete(() => onComplete?.Invoke());
		}

		public static void DoSmoothnessValue(this Material mat, float value, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			DOTween.To(() => mat.Smoothness(), x => mat.SetSmoothness(x), value, time)
		   .SetEase(ease)
		   .SetUpdate(updateType)
		   .OnComplete(() => onComplete?.Invoke());
		}

		#region MaterialPropertyBlock Functions

		//public static void FadePropertyBlock(this Renderer rend, float fadeValue, float time, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, Action doOnComplete = null)
		//{
		//	for (int i = 0; i < rend.materials.Length; i++)
		//	{
		//		MaterialPropertyBlock block = rend.GetPropertyBlock(i);

		//		if (block == null)
		//		{
		//			rend.materials[i].DOFade(fadeValue, time)
		//			.SetEase(ease)
		//			.SetUpdate(updateType)
		//			.OnComplete(() => doOnComplete?.Invoke());
		//		}
		//		else
		//		{
		//			Color targetColor = rend.materials[i].color;
		//			targetColor.a = fadeValue;

		//			DOTween.To(() => rend.GetPropertyBlock(i).Color(), x =>
		//			{
		//				MaterialPropertyBlock propertyBlock = rend.GetPropertyBlock(i);
		//				propertyBlock.SetColor(x);
		//				rend.SetPropertyBlock(propertyBlock, i);
		//			},
		//			targetColor, time)
		//		   .SetEase(ease)
		//		   .SetUpdate(updateType)
		//		  .OnComplete(() => doOnComplete?.Invoke());
		//		}
		//	}
		//}

		public static void SetSmoothness(this MaterialPropertyBlock block, float value) => block.SetFloat(RofoUtil.PROP_SMOOTHNESS, value);

		public static void SetMetallic(this MaterialPropertyBlock block, float value) => block.SetFloat(RofoUtil.PROP_METALLIC, value);

		public static void SetEmissionColor(this MaterialPropertyBlock block, Color value) => block.SetColor(RofoUtil.PROP_EMISSION_COLOR, value);

		public static void SetColor(this MaterialPropertyBlock block, Color value) => block.SetColor(RofoUtil.PROP_COLOR, value);

		public static float Metallic(this MaterialPropertyBlock block) => block.GetFloat(RofoUtil.PROP_METALLIC);

		public static float Smoothness(this MaterialPropertyBlock block) => block.GetFloat(RofoUtil.PROP_SMOOTHNESS);

		public static void DoMetallicValue(this Renderer rend, float value, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			if (rend.HasPropertyBlock())
			{
				DOTween.To(() => rend.GetPropertyBlock().Metallic(), x =>
				{
					MaterialPropertyBlock propertyBlock = rend.GetPropertyBlock();
					propertyBlock.SetMetallic(x);
					rend.SetPropertyBlock(propertyBlock);

				}, value, time)
			 .SetEase(ease)
			 .SetUpdate(updateType)
			 .OnComplete(() => onComplete?.Invoke());
			}
			else
				rend.material.DoMetallicValue(value, ease, updateType, time, onComplete);
		}

		public static void DoColor(this Renderer rend, Color targetColor, int materialIndex = 0, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			if (rend.HasPropertyBlock())
			{
				DOTween.To(() => rend.GetPropertyBlock(materialIndex).Color(), x =>
				{
					MaterialPropertyBlock propertyBlock = rend.GetPropertyBlock();
					propertyBlock.SetColor(x);
					rend.SetPropertyBlock(propertyBlock);
				},
				targetColor, time)
			   .SetEase(ease)
			   .SetUpdate(updateType)
			   .OnComplete(() => onComplete?.Invoke());

			}
			else
			{
				rend.materials[materialIndex].DOColor(targetColor, time)
				.SetEase(ease)
				.SetUpdate(updateType)
				.OnComplete(() =>
				{
					onComplete?.Invoke();
				});
			}
		}

		public static void DoEmissionColor(this Renderer rend, Color targetColor, int materialIndex = 0, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			if (rend.HasPropertyBlock())
			{
				DOTween.To(() => rend.GetPropertyBlock(materialIndex).EmissionColor(), x =>
				{
					MaterialPropertyBlock propertyBlock = rend.GetPropertyBlock();
					propertyBlock.SetEmissionColor(x);
					rend.SetPropertyBlock(propertyBlock);

				}, targetColor, time)
				.SetEase(ease)
				.SetUpdate(updateType)
				.OnComplete(() => onComplete?.Invoke());
			}
			else
			{
				rend.materials[materialIndex].DOColor(targetColor, RofoUtil.PROP_EMISSION_COLOR, time)
					.SetEase(ease)
					.SetUpdate(updateType)
					.OnComplete(() => onComplete?.Invoke());
			}

		}

		public static MaterialPropertyBlock GetPropertyBlock(this Renderer rend, int materialIndex = 0)
		{
			MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
			rend.GetPropertyBlock(propertyBlock, materialIndex);
			return propertyBlock;
		}

		public static void DoSmoothnessValue(this Renderer rend, float value, Ease ease = Ease.Linear, UpdateType updateType = UpdateType.Fixed, float time = 1, Action onComplete = null)
		{
			if (rend.HasPropertyBlock())
			{
				DOTween.To(() => rend.GetPropertyBlock().Smoothness(), x =>
				{
					MaterialPropertyBlock propertyBlock = rend.GetPropertyBlock();
					propertyBlock.SetSmoothness(x);
					rend.SetPropertyBlock(propertyBlock);

				}, value, time)
				.SetEase(ease)
				.SetUpdate(updateType)
				.OnComplete(() => onComplete?.Invoke());
			}
			else
				rend.material.DoSmoothnessValue(value, ease, updateType, time, onComplete);

		}

		public static void SetTexture(this MaterialPropertyBlock block, Texture2D value) => block.SetTexture(RofoUtil.PROP_MAIN_TEXTURE, value);

		public static MaterialPropertyBlock SetMaterialPropertyBlock(this Renderer rend, MaterialPropertyInfo materialPropertyInfo = null)
		{
			if (materialPropertyInfo == null)
				return null;

			MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

			if (materialPropertyInfo.mainColor != default)
				propertyBlock.SetColor(materialPropertyInfo.mainColor);

			if (materialPropertyInfo.emissionColor != default)
				propertyBlock.SetEmissionColor(materialPropertyInfo.emissionColor);

			if (materialPropertyInfo.albedoTexture != null)
				propertyBlock.SetTexture(materialPropertyInfo.albedoTexture);

			if (materialPropertyInfo.smoothness != -1)
				propertyBlock.SetSmoothness(materialPropertyInfo.smoothness);

			if (materialPropertyInfo.metallic != -1)
				propertyBlock.SetMetallic(materialPropertyInfo.metallic);

			rend.SetPropertyBlock(propertyBlock, materialPropertyInfo.materialIndex);

			return propertyBlock;
		}

		#endregion MaterialPropertyBlock Functions
	}
}