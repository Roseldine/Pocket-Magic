
using UnityEngine;

public static class VectorExtensions
{
    // set/change SINGLE value
    private static Vector3 SetIndexValue(this Vector3 vector, int axis, float value)
    {
        vector[axis] = value;
        return vector;
    }

    public static Vector3 SetX(this Vector3 vector, float xValue)
        => SetIndexValue(vector, 0, xValue);

    public static Vector3 SetY(this Vector3 vector, float yValue)
        => SetIndexValue(vector, 1, yValue);

    public static Vector3 SetZ(this Vector3 vector, float zValue)
        => SetIndexValue(vector, 2, zValue);



    // set/change TWO values
    public static Vector3 SetIndexValue(this Vector3 vector, int axis1, float value1, int axis2, float value2)
    {
        vector[axis1] = value1;
        vector[axis2] = value2;
        return vector;
    }

    public static Vector3 SetXY(this Vector3 vector, float xValue, float yValue)
        => SetIndexValue(vector, 0, xValue, 1, yValue);

    public static Vector3 SetXZ(this Vector3 vector, float xValue, float zValue)
        => SetIndexValue(vector, 0, xValue, 2, zValue);

    public static Vector3 SetYZ(this Vector3 vector, float yValue, float zValue)
        => SetIndexValue(vector, 1, yValue, 2, zValue);



    // get Vector2 from vector3
    public static Vector2 GetVector2(this Vector3 vector, int axis1, int axis2)
        => new Vector2(vector[axis1], vector[axis2]);

    public static Vector2 GetXY(this Vector3 vector)
        => GetVector2(vector, 0, 1);

    public static Vector2 GetXZ(this Vector3 vector)
        => GetVector2(vector, 0, 1);

    public static Vector2 GetYZ(this Vector3 vector)
        => GetVector2(vector, 1, 2);



    public static Vector3 GetMidPoint(this Vector3 vector1, Vector3 vector2)
        => (vector1 + vector2) / 2;



    #region Vector 2
    // set/change SINGLE value
    private static Vector2 SetIndexValue(this Vector2 vector, int axis, float value)
    {
        vector[axis] = value;
        return vector;
    }

    public static Vector2 SetX(this Vector2 vector, float xValue)
        => SetIndexValue(vector, 0, xValue);

    public static Vector2 SetY(this Vector2 vector, float yValue)
        => SetIndexValue(vector, 1, yValue);

    public static Vector2 SetXY(this Vector2 vector, float xValue, float yValue)
    {
        vector.x = xValue;
        vector.y = yValue;

        return vector;
    }

    public static Vector2 GetMidPoint(this Vector2 vector1, Vector2 vector2)
        => (vector1 + vector2) / 2;

    #endregion // vector 2
}