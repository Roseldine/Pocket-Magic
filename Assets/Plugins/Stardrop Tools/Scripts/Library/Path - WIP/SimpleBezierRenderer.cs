using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    //[ExecuteInEditMode]
    public class SimpleBezierRenderer : CoreComponent
    {
        public enum EBezierType { quadratic, quadraticThroughPoints, cubic, multiple }

        [SerializeField] EBezierType type;
        [SerializeField] LineRenderer line;
        [SerializeField] int resolution = 16;
        [Space]
        [SerializeField] Transform paretPoints;
        [SerializeField] Transform[] controlPoints;
        [SerializeField] List<Vector3> points;

        [Header("Debug")]
        [SerializeField] bool gizmoThreePointPassThroughPoints;

        public void GenerateBezier()
        {
            if (paretPoints != null && paretPoints.childCount != controlPoints.Length)
                controlPoints = Utilities.GetItems<Transform>(paretPoints);

            if (type == EBezierType.quadratic)
                RenderBezierQuadratic();
            else if (type == EBezierType.quadraticThroughPoints)
                RenderBezierQuadraticThroughPoints();
            else if (type == EBezierType.multiple)
                RenderInfinite();

            RefreshLine();
        }

        void RenderBezierQuadratic()
        {
            points = new List<Vector3>();
            float t = 0;

            for (int i = 0; i < resolution; i++)
            {
                t = i / (float)resolution;
                points.Add(SimpleBezier.QuadraticBezierInterpolation(controlPoints[0], controlPoints[1], controlPoints[2], t));
            }

            points.Add(controlPoints[controlPoints.Length - 1].position);
        }

        /// <summary>
        /// A list of points from a group of beziers that pass on top of control points.
        /// Generates a bezier for every 2 points.
        /// <para> ---- AT LEAST 3 controll points! </para>
        /// </summary>
        public void QuadraticBezierThatPassesThroughGivenPoints()
        {
            

        }


        void RenderBezierQuadraticThroughPoints()
        {
            //return;

            points = new List<Vector3>();
            points = SimpleBezier.ThreePointQuadraticBezierPassThroughPoints(controlPoints, resolution);
        }

        void RenderInfinite()
        {
            points = new List<Vector3>();
            float t = 0;
            for (int i = 0; i < resolution; i++)
            {
                t = (float)i / resolution;
                points.Add(SimpleBezier.MultipleBezierPointControllerInterpolation(controlPoints, t));
            }
        }

        void RefreshLine()
        {
            if (line == null)
                return;

            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
        }

        public void Update()
        {
            GenerateBezier();
        }

        private void OnValidate()
        {
            GenerateBezier();
        }

        

        private void OnDrawGizmos()
        {
            GenerateBezier();

            if (points != null && points.Count > 1)
            {
                Gizmos.color = Color.cyan;
                for (int i = 0; i < points.Count - 1; i++)
                    Gizmos.DrawLine(points[i], points[i + 1]);
                Gizmos.DrawLine(points[points.Count - 1], controlPoints[controlPoints.Length - 1].position);

                #region Pass Through Points
                // Pass through Points
                if (gizmoThreePointPassThroughPoints && type == EBezierType.quadraticThroughPoints)
                {
                    Vector3[] points = new Vector3[controlPoints.Length];
                    for (int i = 0; i < controlPoints.Length; i++)
                        points[i] = controlPoints[i].position;

                    int resolutionPerBezier = resolution;

                    if (points.Length < 3)
                    {
                        Debug.LogError("Requires 3 points!");
                        return;
                    }

                    List<Vector3> bezierPoints = new List<Vector3>();

                    if (resolutionPerBezier < 1)
                        resolutionPerBezier = 1;

                    Vector3 centerPoint = VectorUtility.GetMidPoint(points[0], points[points.Length - 1]);
                    Vector3 p1, p2, p3, midThreePoints;
                    Vector3 midA, midB, midC;
                    Vector3 direction, controllerPos;

                    float time = 0;

                    // loop through points 
                    for (int i = 0; i < points.Length - 2; i++)
                    {
                        // get points
                        p1 = points[i];
                        p2 = points[i + 1];
                        p3 = points[i + 2];

                        // find mid point & direction from bezier center
                        Vector3[] pointArrayToMid = { p1, p2, p3 };
                        midThreePoints = VectorUtility.GetMidPoint(pointArrayToMid);
                        midA = VectorUtility.GetMidPoint(p1, p2);
                        midB = VectorUtility.GetMidPoint(p2, p3);
                        midC = VectorUtility.GetMidPoint(midA, midB);

                        direction = midA - midThreePoints;
                        //direction = midC - midThreePoints;

                        // calculate controller position based on direction from center
                        controllerPos = midA + direction.normalized * direction.magnitude;

                        // generate the bezier curve with p1, p2 & controller position
                        for (int j = 0; j < resolutionPerBezier; j++)
                        {
                            time = (float)j / resolutionPerBezier;
                            Vector3 point = SimpleBezier.QuadraticBezierInterpolation(p1, controllerPos, p2, time);
                            bezierPoints.Add(point);
                        }

                        this.points = bezierPoints;

                        Gizmos.color = Color.black;
                        Gizmos.DrawSphere(centerPoint, .2f);
                        Gizmos.DrawSphere(midThreePoints, .15f);

                        Gizmos.color = Color.blue;
                        Gizmos.DrawSphere(p1, .125f);
                        Gizmos.DrawSphere(p2, .125f);
                        Gizmos.DrawSphere(p3, .125f);

                        Gizmos.color = Color.white;
                        Gizmos.DrawSphere(midA, .125f);
                        Gizmos.DrawSphere(midB, .125f);
                        Gizmos.DrawSphere(midC, .15f);

                        Gizmos.color = Color.red;
                        Gizmos.DrawRay(midThreePoints, midA - midThreePoints);
                        //Gizmos.DrawRay(midThreePoints, midB - midThreePoints);

                        
                    }
                }
                #endregion // pass through
            }
        }
    }
}