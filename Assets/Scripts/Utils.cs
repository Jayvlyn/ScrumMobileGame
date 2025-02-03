using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public static class Utils
{
    public static Vector2[] GetDirectionsInCircle2D(int num, float angle)
    {
        List<Vector2> result = new List<Vector2>();

        // if odd number, set first direction as up (0, 1)
        if (num % 2 == 1) result.Add(Vector2.up);

        // compute the angle between rays
        float angleOffset = (angle * 2) / num;

        // add the +/- directions around the circle
        for (int i = 1; i <= num / 2; i++)
        {
            float modifier = (i == 1 && num % 2 == 0) ? 0.65f : 1;

            result.Add(Quaternion.AngleAxis(+angleOffset * i * modifier, Vector3.forward) * Vector2.up);

            result.Add(Quaternion.AngleAxis(-angleOffset * i * modifier, Vector3.forward) * Vector2.up);
        }

        return result.ToArray();
    }


	public static float RandomBinomial()
	{
        return Random.Range(0f, 1f) - Random.Range(0f, 1f); 
	}

    public static Vector2 RandomUnitVector2()
    {
        float random = Random.Range(0f, 260f);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public static Vector3 RandomUnitVector3()
    {
        float random = Random.Range(0f, 260f);
        return new Vector3(Mathf.Cos(random), Mathf.Sin(random), 0);
    }

    public static bool IsPositionInCameraBounds(Vector3 position)
    {
        // Convert the position to viewport coordinates
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

        // Check if the position is within the camera's viewport
        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z >= 0; // z should be >= 0 to ensure the position is in front of the camera
    }


	public static int GetWeightedRandomNumber(int min, int max)
	{
		float randomValue = Random.Range(0f, 1f);

		// Invert the random value to favor smaller numbers
		float scaledValue = 1f - Mathf.Sqrt(randomValue);

		// Scale to the desired range
		return Mathf.FloorToInt(scaledValue * (max - min + 1)) + min;
	}

}
