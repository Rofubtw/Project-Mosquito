using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum MovementType
{
	BasicMovement,
	Circular,
	Oscillation,
	Slice,
	Scale
}

public enum MovementDirection
{
	Up = 1,
	Down = -1,
	Right = 2,
	Left = -2,
	Forth = 3,
	Back = -3
}

public enum RotationAxis
{
	X,
	Y,
	Z
}

public enum RotationDirection
{
	Clockwise = -1,
	CounterClockWise = 1
}

public class TweenController : MonoBehaviour
{
	[SerializeField, EnumToggleButtons, HideLabel] private MovementType type = MovementType.Circular;
	[SerializeField] private Ease ease = Ease.Linear;
	[SerializeField] private float tweenTime = 1f;
	[SerializeField] private bool randomDelay = false;
	[SerializeField, HideIf("randomDelay")] private float delay;
	[SerializeField, ShowIf("randomDelay")] private Vector2 randomDelayLimits;

	[SerializeField, ShowIf("@type == MovementType.Circular")] private float rotationSpeed = 30f;
	[SerializeField, ShowIf("@type == MovementType.Scale ")] private Vector3 scale = Vector3.one;
	[SerializeField, HideIf("@type == MovementType.BasicMovement|| type == MovementType.Scale")] private RotationAxis rotationAxis = RotationAxis.Y;
	[SerializeField, ShowIf("@type == MovementType.BasicMovement")] private MovementDirection movementDirection = MovementDirection.Right;
	[SerializeField, ShowIf("@type == MovementType.BasicMovement")] private float movementRange;
	[SerializeField, ShowIf("@type == MovementType.BasicMovement")] private bool symmetrical = false;
	[SerializeField, ShowIf("@type == MovementType.Oscillation || type == MovementType.Slice")] private float maxDegree = 45f;

	private ITweenStrategy _tweenStrategy;

	void Start()
	{
		InitializeStrategy();
		SetMovement();
	}

	private void InitializeStrategy()
	{
		switch (type)
		{
			case MovementType.BasicMovement:
				_tweenStrategy = new BasicMovementStrategy(transform, movementDirection, movementRange, symmetrical);
				break;
			case MovementType.Circular:
				_tweenStrategy = new CircularStrategy(transform, rotationAxis, rotationSpeed);
				break;
			case MovementType.Oscillation:
				_tweenStrategy = new OscillationStrategy(transform, rotationAxis, maxDegree);
				break;
			case MovementType.Slice:
				_tweenStrategy = new SliceStrategy(transform, rotationAxis, maxDegree);
				break;
			case MovementType.Scale:
				_tweenStrategy = new ScaleStrategy(transform, scale);
				break;
			default:
				Debug.LogError("Undefined movement type!");
				break;
		}
	}

	private void SetMovement()
	{
		float finalDelay = randomDelay ? Random.Range(randomDelayLimits.x, randomDelayLimits.y) : delay;
		_tweenStrategy?.Execute(tweenTime, ease, finalDelay, GetHashCode());
	}

	public void SetDelayTime(float delayTime)
	{
		delay = delayTime;
		randomDelay = false;
	}

	public void KillTweens()
	{
		DOTween.Kill(GetHashCode());
	}
}
