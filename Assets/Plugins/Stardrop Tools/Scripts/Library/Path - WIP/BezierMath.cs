using System.Collections;
using UnityEngine;

namespace StardropTools.Spline
{
    public static class BezierMath
    {
        /// <summary>
        /// Gets a point in a Quadratic Bezier Curve at a given time
        /// </summary>
        public static Vector3 QuadraticGetPointInTime(Vector3 p0_Start, Vector3 p1_Control, Vector3 p2_Target, float t)
        {
            float oneMinusT = 1f - t;
            return  (oneMinusT * oneMinusT * p0_Start) +
                    (2f * oneMinusT * t * p1_Control) +
                    (t * t * p2_Target);
        }


        /// <summary>
        /// Gets a point in a Cubic Bezier Curve at a given time
        /// </summary>
        public static Vector3 CubicGetPointInTime(Vector3 p0_Start, Vector3 p1_Control, Vector3 p2_Control, Vector3 p3_Target, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;

            return  oneMinusT * oneMinusT * oneMinusT * p0_Start +
                    3f * oneMinusT * oneMinusT * t * p1_Control +
                    3f * oneMinusT * t * t * p2_Control +
                    t * t * t * p3_Target;
        }

        /// <summary>
        /// Direction of curve in given time
        /// (First Derivative)
        /// </summary>
        public static Vector3 CubicGetDirection(Vector3 p0_Start, Vector3 p1_Control, Vector3 p2_Control, Vector3 p3_Target, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;

            return  3f * oneMinusT * oneMinusT * (p1_Control - p0_Start) +
                    6f * oneMinusT * t * (p2_Control - p1_Control) +
                    3f * t * t * (p3_Target - p2_Control);
        }
    }
}