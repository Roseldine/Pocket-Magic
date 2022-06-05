
// 3D adaptation of this script:
// https://github.com/wmcnamara/unity-bezier/blob/main/Assets/Bezier/Bezier.cs

using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public static class SimpleBezier
    {
        /// <summary>
        /// Interpolates between three control points with a quadratic bezier curve, at time t
        /// </summary>
        public static Vector3 QuadraticBezierInterpolation(Transform startPoint, Transform controlPoint, Transform targetPoint, float t)
        {
            Vector3 a = Vector3.Lerp(startPoint.position, controlPoint.position, t);
            Vector3 b = Vector3.Lerp(controlPoint.position, targetPoint.position, t);
            Vector3 bezierPoint = Vector3.Lerp(a, b, t);

            return bezierPoint;
        }

        /// <summary>
        /// Interpolates between three control points with a quadratic bezier curve, at time t
        /// </summary>
        public static Vector3 QuadraticBezierInterpolation(Vector3 startPosition, Vector3 controlPosition, Vector3 targetPosition, float t)
        {
            Vector3 a = Vector3.Lerp(startPosition, controlPosition, t);
            Vector3 b = Vector3.Lerp(controlPosition, targetPosition, t);
            Vector3 bezierPoint = Vector3.Lerp(a, b, t);

            return bezierPoint;
        }

        /// <summary>
        /// A list of points from a group of beziers that pass on top of control points.
        /// <para> ---- ONLY 3 controll points! </para>
        /// </summary>
        public static List<Vector3> ThreePointQuadraticBezierPassThroughPoints(Vector3[] points, int resolutionPerBezier)
        {
            if (points.Length < 3)
            {
                Debug.LogError("Requires 3 points!");
                return null;
            }

            List<Vector3> bezierPoints = new List<Vector3>();

            if (resolutionPerBezier < 1)
                resolutionPerBezier = 1;

            Vector3 centerPoint = VectorUtility.GetMidPoint(points[0], points[points.Length - 1]);
            Vector3 p1, p2, midPoint;
            Vector3 direction, controllerPos;

            float time = 0;

            // loop through points 
            for (int i = 0; i < points.Length - 2; i++)
            {
                // get points
                p1 = points[i];
                p2 = points[i + 1];

                // find mid point & direction from bezier center
                Vector3[] pointArrayToMid = { p1, p2 };
                midPoint = VectorUtility.GetMidPoint(pointArrayToMid);

                direction = centerPoint - midPoint;

                // calculate controller position based on direction from center
                controllerPos = centerPoint + direction.normalized * direction.magnitude;

                // generate the bezier curve with p1, p2 & controller position
                for (int j = 0; j < resolutionPerBezier; j++)
                {
                    time = (float)j / resolutionPerBezier;
                    Vector3 point = QuadraticBezierInterpolation(p1, controllerPos, p2, time);
                    bezierPoints.Add(point);
                }
            }

            return bezierPoints;
        }

        /// <summary>
        /// A list of points from a group of beziers that pass on top of control points.
        /// <para> ---- ONLY 3 controll points! </para>
        /// </summary>
        public static List<Vector3> ThreePointQuadraticBezierPassThroughPoints(Transform[] points, int resolutionPerBezier)
        {
            if (points.Length < 3 || points.Length > 3)
            {
                Debug.LogError("Only accepts 3 points!");
                return null;
            }
            

            // convert Transform to Vector3
            Vector3[] positions = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
                positions[i] = points[i].position;


            List<Vector3> bezierPoints = new List<Vector3>();

            if (resolutionPerBezier < 1)
                resolutionPerBezier = 1;

            Vector3 centerPoint = VectorUtility.GetMidPoint(points[0], points[points.Length - 1]);
            Vector3 p1, p2, midPoint;
            Vector3 direction, controllerPos;

            float time = 0;

            // loop through points 
            for (int i = 0; i < points.Length - 2; i++)
            {
                // get points
                p1 = points[i].position;
                p2 = points[i + 1].position;

                // find mid point & direction from bezier center
                midPoint = VectorUtility.GetMidPoint(p1, p2);
                direction = centerPoint - midPoint;

                // calculate controller position based on direction from center
                controllerPos = centerPoint + direction.normalized * direction.magnitude;

                // generate the bezier curve with p1, p2 & controller position
                for (int j = 0; j < resolutionPerBezier; j++)
                {
                    time = (float)j / resolutionPerBezier;
                    Vector3 point = QuadraticBezierInterpolation(p1, controllerPos, p2, time);
                    bezierPoints.Add(point);
                }
            }

            return bezierPoints;
        }




        /// <summary>
        /// Interpolates between four control points with a cubic bezier curve, at time t
        /// </summary>
        public static Vector3 CubicBezierInterpolation(Transform startPoint, Transform controlPointA, Transform controlPointB, Transform targetPoint, float t)
        {
            Vector3 a = Vector3.Lerp(startPoint.position, controlPointA.position, t);
            Vector3 b = Vector3.Lerp(controlPointA.position, controlPointB.position, t);
            Vector3 c = Vector3.Lerp(controlPointB.position, targetPoint.position, t);

            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            Vector3 bezierPoint = Vector3.Lerp(d, e, t);

            return bezierPoint;
        }

        /// <summary>
        /// Interpolates between four control points with a cubic bezier curve, at time t
        /// </summary>
        public static Vector3 CubicBezierInterpolation(Vector3 startPoint, Vector3 controlPointA, Vector3 controlPointB, Vector3 targetPoint, float t)
        {
            Vector3 a = Vector3.Lerp(startPoint, controlPointA, t);
            Vector3 b = Vector3.Lerp(controlPointA, controlPointB, t);
            Vector3 c = Vector3.Lerp(controlPointB, targetPoint, t);

            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            Vector3 bezierPoint = Vector3.Lerp(d, e, t);

            return bezierPoint;
        }



        /// <summary>
        /// Interpolates between any number of control points in the points list, using a bezier curve and the interpolant, t. 
        /// </summary>
        public static Vector3 MultipleBezierPointControllerInterpolation(List<Transform> points, float t)
            => MultipleBezierPointControllerInterpolation(points.ToArray(), t);

        /// <summary>
        /// Interpolates between any number of control points in the points list, using a bezier curve and the interpolant, t. 
        /// </summary>
        public static Vector3 MultipleBezierPointControllerInterpolation(Transform[] points, float t)
        {
            if (points.Length < 2)
                throw new System.Exception("Bezier Curve needs atleast 3 points, or 2 for a linear interpolation");

            //Convert the list of Transform's to a list of Vector3
            List<Vector3> positions = new List<Vector3>();
            foreach (Transform p in points)
                positions.Add(p.position);
            
            return MassPointLerper(positions, t);
        }

        /// <summary>
        /// Interpolates between any number of control points in the points list, using a bezier curve and the interpolant, t. 
        /// </summary>
        public static Vector3 MultipleBezierPointControllerInterpolation(Vector3[] points, float t)
        {
            if (points.Length < 2)
                throw new System.Exception("Bezier Curve needs atleast 3 points, or 2 for a linear interpolation");

            return MassPointLerper(points, t);
        }



        /// <summary>
        /// Basically, just lerp through every point at index i with i + 1
        /// </summary>
        private static Vector3 MassPointLerper(List<Vector3> points, float t)
            => MassPointLerper(points.ToArray(), t);


        /// <summary>
        /// Basically, just lerp through every point at index (i) with next index (i + 1)
        /// </summary>
        private static Vector3 MassPointLerper(Vector3[] points, float t)
        {
            if (points.Length == 2)
                return Vector3.Lerp(points[0], points[1], t);

            List<Vector3> lines = new List<Vector3>();

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 line = Vector3.Lerp(points[i], points[i + 1], t);
                lines.Add(line);
            }

            return MassPointLerper(lines.ToArray(), t);
        }
    }
}