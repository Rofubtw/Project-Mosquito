using DG.Tweening;
using UnityEngine;
using Extensions;

public interface ITweenStrategy
{
	void Execute(float duration, Ease ease, float delay, int tweenId);
}

public class BasicMovementStrategy : ITweenStrategy
{
	private readonly Transform _transform;
	private readonly Vector3 _axis;
	private readonly float _range;
	private readonly bool _symmetrical;

	public BasicMovementStrategy(Transform transform, MovementDirection direction, float range, bool symmetrical)
	{
		_transform = transform;
		_axis = direction switch
		{
			MovementDirection.Left => Vector3.left,
			MovementDirection.Right => Vector3.right,
			MovementDirection.Up => Vector3.up,
			MovementDirection.Down => Vector3.down,
			MovementDirection.Forth => Vector3.forward,
			MovementDirection.Back => Vector3.back,
			_ => Vector3.zero
		};
		_range = range;
		_symmetrical = symmetrical;
	}

	public void Execute(float duration, Ease ease, float delay, int tweenId)
	{
		if (_symmetrical)
			_transform.localPosition -= _axis * _range;

		_transform.DOLocalMove(_axis * _range * (_symmetrical ? 2 : 1), duration)
			.SetRelative()
			.SetEase(ease)
			.SetLoops(-1, LoopType.Yoyo)
			.CommonTweenOptions(delay, tweenId);
	}
}

public class CircularStrategy : ITweenStrategy
{
	private readonly Transform _transform;
	private readonly RotationAxis _axis;
	private readonly float _rotationSpeed;

	public CircularStrategy(Transform transform, RotationAxis axis, float speed)
	{
		_transform = transform;
		_axis = axis;
		_rotationSpeed = speed;
	}

	public void Execute(float duration, Ease ease, float delay, int tweenId)
	{
		Vector3 axisVec = AxisToVector(_axis);
		_transform.DOLocalRotate(axisVec * _rotationSpeed, duration)
			.SetEase(ease)
			.SetRelative()
			.SetLoops(-1, LoopType.Incremental)
			.CommonTweenOptions(delay, tweenId);
	}

	private Vector3 AxisToVector(RotationAxis axis) =>
		axis switch
		{
			RotationAxis.X => Vector3.right,
			RotationAxis.Y => Vector3.up,
			RotationAxis.Z => Vector3.forward,
			_ => Vector3.zero
		};
}

public class OscillationStrategy : ITweenStrategy
{
	private readonly Transform _transform;
	private readonly Vector3 _axis;
	private readonly float _maxDegree;

	public OscillationStrategy(Transform transform, RotationAxis axis, float maxDegree)
	{
		_transform = transform;
		_axis = axis switch
		{
			RotationAxis.X => Vector3.right,
			RotationAxis.Y => Vector3.up,
			RotationAxis.Z => Vector3.forward,
			_ => Vector3.up
		};
		_maxDegree = maxDegree;
	}

	public void Execute(float duration, Ease ease, float delay, int tweenId)
	{
		_transform.DOLocalRotate(_axis * _maxDegree, duration / 2)
			.SetEase(ease)
			.SetDelay(delay)
			.OnComplete(() =>
			{
				_transform.DOLocalRotate(-_axis * _maxDegree, duration)
					.SetEase(ease)
					.SetLoops(-1, LoopType.Yoyo)
					.CommonTweenOptions(0, tweenId);
			})
			.SetId(tweenId)
			.SetUpdate(UpdateType.Fixed, true);
	}
}

public class SliceStrategy : ITweenStrategy
{
	private readonly Transform _transform;
	private readonly Vector3 _axis;
	private readonly float _maxDegree;

	public SliceStrategy(Transform transform, RotationAxis axis, float maxDegree)
	{
		_transform = transform;
		_axis = axis switch
		{
			RotationAxis.X => Vector3.right,
			RotationAxis.Y => Vector3.up,
			RotationAxis.Z => Vector3.forward,
			_ => Vector3.up
		};
		_maxDegree = maxDegree;
	}

	public void Execute(float duration, Ease ease, float delay, int tweenId)
	{
		_transform.DOLocalRotate(_axis * _maxDegree, duration)
			.SetEase(ease)
			.SetLoops(-1, LoopType.Yoyo)
			.SetEase(ease, 1f)
			.CommonTweenOptions(delay, tweenId);
	}
}

public class ScaleStrategy : ITweenStrategy
{
	private readonly Transform _transform;
	private readonly Vector3 _targetScale;

	public ScaleStrategy(Transform transform, Vector3 targetScale)
	{
		_transform = transform;
		_targetScale = targetScale;
	}

	public void Execute(float duration, Ease ease, float delay, int tweenId)
	{
		_transform.DOScale(_targetScale, duration)
			.SetEase(ease)
			.SetLoops(-1, LoopType.Yoyo)
			.CommonTweenOptions(delay, tweenId);
	}
}