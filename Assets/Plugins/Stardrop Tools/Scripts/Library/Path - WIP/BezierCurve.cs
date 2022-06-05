using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Spline
{
    /// <summary>
    /// List of line points besed on control points
    /// </summary>
    public class BezierCurve
    {
        public Transform[] controlPoints;
        public Vector3[] vectorPath { get; private set; }

        const int resolutionDefault = 16;

        #region Quadratic Constructors
        /// <summary>
        /// Returns a Quadratic Bezier Curve
        /// </summary>
        public BezierCurve(Transform startPoint, Transform controlPoint, Transform targetPoint, int resolution = 16)
        {
            Transform[] p = { startPoint, controlPoint, targetPoint };
            controlPoints = p;
            vectorPath = GetCurvePoints(p, resolution);
        }

        /// <summary>
        /// Returns a Quadratic Bezier Curve
        /// </summary>
        public BezierCurve(Vector3 startPoint, Vector3 controlPoint, Vector3 targetPoint, int resolution = 16)
        {
            Vector3[] p = { startPoint, controlPoint, targetPoint };
            vectorPath = GetCurvePoints(p, resolution);
        }
        #endregion



        #region Cubic constructors
        /// <summary>
        /// Returns a Cubic Bezier Curve
        /// </summary>
        public BezierCurve(Transform startPoint, Transform controlPointA, Transform controlPointB, Transform targetPoint, int resolution = 16)
        {
            Transform[] p = { startPoint, controlPointA, controlPointB, targetPoint };
            controlPoints = p;
            vectorPath = GetCurvePoints(p, resolution);
        }

        /// <summary>
        /// Returns a Cubic Bezier Curve
        /// </summary>
        public BezierCurve(Vector3 startPoint, Vector3 controlPointA, Vector3 controlPointB, Vector3 targetPoint, int resolution = 16)
        {
            Vector3[] p = { startPoint, controlPointA, controlPointB, targetPoint };
            vectorPath = GetCurvePoints(p, resolution);
        }
        #endregion // cubic constructor



        /// <summary>
        /// Requires at least 3 control points!
        /// <para>3 CONTROL POINTS!</para>
        /// </summary>
        public BezierCurve(Transform[] controlPoints, int resolution)
        {
            if (controlPoints.Length < 3)
                return;

            this.controlPoints = controlPoints;
            vectorPath = GetCurvePoints(controlPoints, resolution);
        }



        Vector3[] GetCurvePoints(Vector3[] controlPoints, int resolution)
        {
            if (controlPoints.Length < 3)
            {
                Debug.LogError("Need at least 3 points");
                return null;
            }

            List<Vector3> curvePoints = new List<Vector3>();

            float t = 0;
            for (int i = 0; i < resolution; i++)
            {
                t = (float)i / resolution;

                if (controlPoints.Length == 3)
                    curvePoints.Add(BezierMath.QuadraticGetPointInTime(controlPoints[0], controlPoints[1], controlPoints[2], t));
            }

            return curvePoints.ToArray();
        }

        Vector3[] GetCurvePoints(Transform[] controlPoints, int resolution)
        {
            if (controlPoints.Length < 3)
            {
                Debug.LogError("Need at least 3 points");
                return null;
            }

            // convert transform to points
            Vector3[] ctrlPoints = new Vector3[controlPoints.Length];
            for (int i = 0; i < controlPoints.Length; i++)
                ctrlPoints[i] = controlPoints[i].position;

            return GetCurvePoints(ctrlPoints, resolution);
        }
    }
}