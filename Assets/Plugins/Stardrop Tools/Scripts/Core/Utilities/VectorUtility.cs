using System.Collections.Generic;
using UnityEngine;

public static class VectorUtility
{
    public static float AngleLookAtDirectionXY(Vector3 direction, float offset = 0)
    => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

    public static float AngleLookAtDirectionXZ(Vector3 direction, float offset = 0)
        => Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + offset;

    public static Vector3 GetMidPoint(Transform posOne, Transform posTwo)
        => GetMidPoint(posOne.position, posTwo.position);

    public static Vector3 GetMidPoint(Vector3 vectorOne, Vector3 vectorTwo)
        => (vectorOne + vectorTwo) / 2;

    public static Vector3 GetMidPoint(Vector3[] vectorArray)
    {
        Vector3 totalPoints = Vector3.zero;
        for (int i = 0; i < vectorArray.Length; i++)
            totalPoints += vectorArray[i];

        return totalPoints / vectorArray.Length;
    }


    /// <summary>
    /// Axis is the axis to influence | ex: axis.up = horizonal
    /// </summary>
    public static Vector3 GetPerpendicular(Vector3 direction, Vector3 axis)
        => Vector3.Cross(direction, axis).normalized;

    /// <summary>
    /// dirA = direction start, dirB = direction end | ex: direction = dirB - dirA.
    /// Axis = the axis to influence | ex: axis.up = horizonal
    /// </summary>
    public static Vector3 GetPerpendicular(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        Vector3 direction = dirB - dirA;
        return GetPerpendicular(direction, axis);
    }


    public static Vector3 GetVelocity(Vector3 currentPosition, Vector3 lastPosition)
        => (currentPosition - lastPosition) / Time.deltaTime;

    public static Quaternion SmoothLookAt(Transform target, Vector3 direction, float lookSpeed, bool lockX = true, bool lockY = false, bool lockZ = true)
    {
        if (direction == Vector3.zero)
            return Quaternion.identity;

        Quaternion lookRot = Quaternion.LookRotation(direction);
        Quaternion targetRot = Quaternion.Slerp(target.rotation, lookRot, Time.deltaTime * lookSpeed);

        if (lockX) lookRot.x = 0;
        if (lockY) lookRot.y = 0;
        if (lockZ) lookRot.z = 0;

        target.rotation = targetRot;
        return targetRot;
    }

    public static Vector3 RandomInsideCricle(Vector3 referencePoint, float radius)
    {
        Vector2 circleRandom = Random.insideUnitCircle * radius;
        Vector3 circleRandomVector = new Vector3(circleRandom.x, 0, circleRandom.y);

        return circleRandomVector + referencePoint;
    }

    /// <summary>
    /// Creates a smooth Curve based on provided anchor points (At least 3 points needed)
    /// array = { start, controlPoint, target }
    /// </summary>
    public static Vector3[] MakeSmoothCurve(Vector3[] arrayToCurve, int resolution)
    {
        List<Vector3> points;
        List<Vector3> curvedPoints;
        int pointsLength = 0;
        int curvedLength = 0;

        if (resolution < 1.0f)
            resolution = 1;

        pointsLength = arrayToCurve.Length;

        curvedLength = pointsLength * resolution - 1;
        curvedPoints = new List<Vector3>(curvedLength);

        float t = 0.0f;
        for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
        {
            t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

            points = new List<Vector3>(arrayToCurve);

            for (int j = pointsLength - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    points[i] = (1 - t) * points[i] + t * points[i + 1];
                }
            }

            curvedPoints.Add(points[0]);
        }

        return (curvedPoints.ToArray());
    }

    public static Vector3[] MakeSmoothCurve(Vector3 startPosition, Vector3 controllerPosition, Vector3 targetPosition, int resolution)
    {
        Vector3[] array = { startPosition, controllerPosition, targetPosition };
        return MakeSmoothCurve(array, resolution);
    }

    //public static Vector3[] MakeSmoothCurvePassingThroughPoints()


    /// <summary>
    /// Create a point circle with designated rotation vector
    /// direction; 1 = left, -1 right
    /// </summary>
    public static Vector3[] CreatePointCircle(Vector3 centerPos, Vector3 targetRotation, int vertexNumber, float radius, int direction = -1)
    {
        Vector3[] points = new Vector3[vertexNumber];
        float angle = 2 * direction * Mathf.PI / vertexNumber;
        Vector3 initialRelativePosition = new Vector3(radius, 0, 0); // orbit.targetAngle * radius;

        Quaternion rotation = Quaternion.Euler(targetRotation);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                                     new Vector4(0, 0, 1, 0),
                                                     new Vector4(0, 0, 0, 1));

            points[i] = centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition);
            Vector3 vector = points[i];
            points[i] = m.MultiplyPoint3x4(vector);
        }

        return points;
    }

    /// <summary>
    /// Create a point circle with designated rotation vector
    /// direction; 1 = left, -1 right
    /// </summary>
    public static Vector3[] CreatePointCircleHorizontal(Vector3 centerPos, int vertexNumber, float radius, int direction = -1)
        => CreatePointCircle(centerPos, new Vector3(90, 0, 90), vertexNumber, radius, direction);

    /// <summary>
    /// Creates a point circle with adjustable angle (0 - 360)
    /// </summary>
    public static Vector3[] CreateAnglePointCircle(float startAngle, float endAngle, float radius, int resolution, Vector3 targetRotation)
    {
        List<Vector3> arcPoints = new List<Vector3>();
        float angle = startAngle;
        float arcLength = endAngle - startAngle;

        Quaternion rotation = Quaternion.Euler(targetRotation);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);

        for (int i = 0; i <= resolution; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            arcPoints.Add(new Vector3(x, y, 0));
            arcPoints[i] = m.MultiplyPoint3x4(arcPoints[i]);

            angle += (arcLength / resolution);
        }

        return arcPoints.ToArray();
    }
}