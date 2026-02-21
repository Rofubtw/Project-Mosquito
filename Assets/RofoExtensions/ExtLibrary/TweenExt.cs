using DG.Tweening;
using UnityEngine;

namespace Extensions
{
	public static class TweenExt
	{
		public static void DoResize(this LineRenderer line, float startWidth, float endWidth, float time, Ease ease = Ease.Linear)
		{
			DOTween.To((value) => { line.startWidth = value; }, line.startWidth, startWidth, time).SetEase(ease);
			DOTween.To((value) => { line.endWidth = value; }, line.endWidth, endWidth, time).SetEase(ease);
		}

		public static Tweener CommonTweenOptions(this Tweener tweener, float delay, int tweenId)
		{
			return tweener
				.SetDelay(delay)
				.SetUpdate(UpdateType.Fixed, true)
				.SetId(tweenId);
		}
	}
}
