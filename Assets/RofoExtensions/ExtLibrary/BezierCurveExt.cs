using UnityEngine;
using Extensions;

public static class BezierCurveExt
{
	/// <summary>
	/// Sets the given transform's position and direction according to given cubic bezier points (must be 4 points!)
	/// </summary>
	/// <param name="myTransform"></param>
	/// <param name="tValue">Relative location on cubic bezier curve</param>
	/// <param name="setLookAt">Sets the given transform's rotation according to cubic curve</param>
	/// <param name="points">Cubic bezier curve points</param>
	public static void SetCubicBezier(this Transform myTransform, float tValue, bool setLookAt, params Transform[] points)
	{
		if (points.Length < 4)
		{
			Log.ErrorMessage("Points array cannot be less than 4");
			return;
		}

		Vector3 pointPos = (Mathf.Pow((1 - tValue), 3) * points[0].position) +
			(3 * Mathf.Pow((1 - tValue), 2) * tValue * points[1].position) +
			(3 * (1 - tValue) * Mathf.Pow(tValue, 2) * points[2].position) +
			(Mathf.Pow(tValue, 3) * points[3].position);
		myTransform.position = pointPos;
		if (setLookAt)
		{
			myTransform.LookAt(GetCubicDirection(tValue, points));
		}
	}

	/// <summary>
	/// The point according to given relative location
	/// </summary>
	/// <param name="tValue">Relative location on cubic bezier curve</param>
	/// <param name="points">Cubic Bezier curve points</param>
	/// <returns>Returns the point according to given relative location</returns>
	public static Vector3 GetCubicBezierPoint(float tValue, params Transform[] points)
	{
		if (points.Length < 4)
		{
			Log.ErrorMessage("Points array cannot be less than 4");
			return Vector3.zero;
		}

		Vector3 pointPos = (Mathf.Pow((1 - tValue), 3) * points[0].position) +
			(3 * Mathf.Pow((1 - tValue), 2) * tValue * points[1].position) +
			(3 * (1 - tValue) * Mathf.Pow(tValue, 2) * points[2].position) +
			(Mathf.Pow(tValue, 3) * points[3].position);
		return pointPos;
	}
	/// <summary>
	/// The forward direction of given relative point
	/// </summary>
	/// <param name="tValue">Relative location on cubic bezier curve</param>
	/// <param name="points">Cubic Bezier curve points</param>
	/// <returns>Returns the forward direction of given relative point</returns>
	public static Vector3 GetCubicDirection(float tValue, params Transform[] points)
	{
		Vector3 dir = (3 * Mathf.Pow((1 - tValue), 2) * (points[1].position - points[0].position)) +
			(6 * (1 - tValue) * tValue * (points[2].position - points[1].position)) +
			(3 * tValue * tValue * (points[3].position - points[2].position));

		return dir;
	}
	/// <summary>
	/// Sets the forward direction of given relative point
	/// </summary>
	/// <param name="myTransform"></param>
	/// <param name="tValue">Relative location on cubic bezier curve</param>
	/// <param name="points">Cubic Bezier curve points</param>
	public static void SetCubicBezierDirection(this Transform myTransform, float tValue, params Transform[] points)
	{
		Vector3 dir = (3 * Mathf.Pow((1 - tValue), 2) * (points[1].position - points[0].position)) +
			(6 * (1 - tValue) * tValue * (points[2].position - points[1].position)) +
			(3 * tValue * tValue * (points[3].position - points[2].position));

		myTransform.LookAt(dir);
	}


	//QUADRATIC

	/// <summary>
	/// Sets the given transform's position and direction according to given quadratic bezier points (must be 3 points!)
	/// </summary>
	/// <param name="myTransform"></param>
	/// <param name="tValue">Relative location on quadratic bezier curve</param>
	/// <param name="setLookAt">Sets the given transform's rotation according to quadratic curve</param>
	/// <param name="points">Quadratic bezier curve points</param>
	public static void SetQuadraticBezier(this Transform myTransform, float tValue, bool setLookAt, params Transform[] points)
	{
		if (points.Length < 3)
		{
			Log.ErrorMessage("Quadratic points array cannot be less than 3");
			return;
		}

		Vector3 pointPos = (Mathf.Pow((1 - tValue), 2) * points[0].position) +
			(2 * (1 - tValue) * tValue * points[1].position) +
			(tValue * tValue * points[2].position);
		myTransform.position = pointPos;
		if (setLookAt)
		{
			myTransform.LookAt(GetQuadraticDirection(tValue, points));
		}
	}

	/// <summary>
	/// The point according to given relative location
	/// </summary>
	/// <param name="tValue">Relative location on quadratic bezier curve</param>
	/// <param name="points">Quadratic Bezier curve points</param>
	/// <returns>Returns the point according to given relative location</returns>
	public static Vector3 GetQuadraticBezierPoint(float tValue, params Transform[] points)
	{
		if (points.Length < 3)
		{
			Log.ErrorMessage("Points array cannot be less than 3");
			return Vector3.zero;
		}

		Vector3 pointPos = (Mathf.Pow((1 - tValue), 2) * points[0].position) +
			(2 * (1 - tValue) * tValue * points[1].position) +
			(tValue * tValue * points[2].position);
		return pointPos;
	}
	/// <summary>
	/// The forward direction of given relative point
	/// </summary>
	/// <param name="tValue">Relative location on quadratic bezier curve</param>
	/// <param name="points">Quadratic Bezier curve points</param>
	/// <returns>Returns the forward direction of given relative point</returns>
	public static Vector3 GetQuadraticDirection(float tValue, params Transform[] points)
	{
		Vector3 dir = (2 * (1 - tValue) * (points[1].position - points[0].position)) +
			(2 * tValue * (points[2].position - points[1].position));

		return dir;
	}
	/// <summary>
	/// Sets the forward direction of given relative point
	/// </summary>
	/// <param name="myTransform"></param>
	/// <param name="tValue">Relative location on quadratic bezier curve</param>
	/// <param name="points">Quadratic Bezier curve points</param>
	public static void SetQuadraticBezierDirection(this Transform myTransform, float tValue, params Transform[] points)
	{
		Vector3 dir = (2 * (1 - tValue) * (points[1].position - points[0].position)) +
			(2 * tValue * (points[2].position - points[1].position));

		myTransform.LookAt(dir);
	}

	//LINEAR

	/// <summary>
	/// Sets the given transform's position and direction according to given linear bezier points (must be 2 points!)
	/// </summary>
	/// <param name="myTransform"></param>
	/// <param name="tValue">Relative location on linear bezier curve</param>
	/// <param name="setLookAt">Sets the given transform's rotation according to linear curve</param>
	/// <param name="points">linear bezier curve points</param>
	public static void SetLinearBezier(this Transform myTransform, float tValue, bool setLookAt, params Transform[] points)
	{
		if (points.Length < 2)
		{
			Log.ErrorMessage("Linear points array cannot be less than 2");
			return;
		}

		Vector3 pointPos = ((1 - tValue) * points[0].position) + (tValue * points[1].position);
		myTransform.position = pointPos;
		if (setLookAt)
		{
			myTransform.LookAt(points[1]);
		}
	}

	/// <summary>
	/// The point according to given relative location
	/// </summary>
	/// <param name="tValue">Relative location on linear bezier curve</param>
	/// <param name="points">linear Bezier curve points</param>
	/// <returns>Returns the point according to given relative location</returns>
	public static Vector3 GetLinearBezierPoint(float tValue, params Transform[] points)
	{
		if (points.Length < 2)
		{
			Log.ErrorMessage("Points array cannot be less than 2");
			return Vector3.zero;
		}

		Vector3 pointPos = ((1 - tValue) * points[0].position) + (tValue * points[1].position);
		return pointPos;
	}


	public static BSpline CreateBSpline(params Transform[] points)
	{
		BSpline bSpline = new BSpline();

		if (points.Length != 0)
		{
			bSpline.positions = new Vector3[points.Length + 4];

			bSpline.times = new float[points.Length + 2];

			bSpline.count = points.Length + 4;

			// Copy positions data (triplicate start and en points so that curve passes trough them.)
			bSpline.positions[0] = bSpline.positions[1] = points[0].position;

			for (int i = 0; i < points.Length; ++i)
			{
				bSpline.positions[i + 2] = points[i].position;
			}

			bSpline.positions[bSpline.count - 1] = bSpline.positions[bSpline.count - 2] = points[points.Length - 1].position;

			// Setup times (subdivide interval to get arrival times at each knot location)
			float dt = (1.0f - 0.0f) / (float)(points.Length + 1);

			bSpline.times[0] = 0.0f;
			for (int i = 0; i < points.Length; ++i)
			{
				bSpline.times[i + 1] = bSpline.times[i] + dt;
			}
			bSpline.times[points.Length + 1] = 1.0f;
		}

		return bSpline;

	}

	public static BSpline UpdateBSpline(this BSpline bSpline, params Transform[] points)
	{
		if (bSpline != null && (bSpline.positions.Length == points.Length + 4) && (bSpline.times.Length == points.Length + 2) && (bSpline.count == points.Length + 4))
		{
			bSpline.positions[0] = bSpline.positions[1] = points[0].position;

			for (int i = 0; i < points.Length; ++i)
			{
				bSpline.positions[i + 2] = points[i].position;
			}

			bSpline.positions[bSpline.count - 1] = bSpline.positions[bSpline.count - 2] = points[points.Length - 1].position;

			// Setup times (subdivide interval to get arrival times at each knot location)
			float dt = (1.0f - 0.0f) / (float)(points.Length + 1);

			bSpline.times[0] = 0.0f;
			for (int i = 0; i < points.Length; ++i)
			{
				bSpline.times[i + 1] = bSpline.times[i] + dt;
			}
			bSpline.times[points.Length + 1] = 1.0f;

			return bSpline;
		}
		else
		{
			bSpline = CreateBSpline(points);
			return bSpline;
		}
	}

	public static Vector3 GetBSplinePoint(this BSpline bSpline, float tValue)
	{
		if (bSpline.count < 6)
		{
			return Vector3.zero;
		}

		// Handle boundry conditions
		if (tValue <= bSpline.times[0])
		{
			return bSpline.positions[0];
		}
		else if (tValue >= bSpline.times[bSpline.count - 3])
		{
			return bSpline.positions[bSpline.count - 3];
		}

		// Find segment and parameter

		int segment = 0;
		while (segment < bSpline.count - 3)
		{

			if (tValue <= bSpline.times[segment + 1])
			{
				break;
			}

			segment++;

		}

		float t0 = bSpline.times[segment];
		float t1 = bSpline.times[segment + 1];
		float u = (tValue - t0) / (t1 - t0);

		// match segment index to standard B-spline terminology
		segment += 3;

		// Evaluate
		Vector3 A = bSpline.positions[segment] - 3.0f * bSpline.positions[segment - 1] + 3.0f * bSpline.positions[segment - 2] - bSpline.positions[segment - 3];
		Vector3 B = 3.0f * bSpline.positions[segment - 1] - 6.0f * bSpline.positions[segment - 2] + 3.0f * bSpline.positions[segment - 3];
		Vector3 C = 3.0f * bSpline.positions[segment - 1] - 3.0f * bSpline.positions[segment - 3];
		Vector3 D = bSpline.positions[segment - 1] + 4.0f * bSpline.positions[segment - 2] + bSpline.positions[segment - 3];

		return (D + u * (C + u * (B + u * A))) / 6.0f;
	}
}

[System.Serializable]
public class BSpline
{
	public Vector3[] positions;
	public float[] times;
	public int count;
}