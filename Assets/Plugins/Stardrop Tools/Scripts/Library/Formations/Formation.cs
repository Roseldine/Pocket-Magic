
using UnityEngine;

namespace StardropTools.Formations
{
    public abstract class Formation : CoreObject
    {
        [Header("Formation")]
        [SerializeField] protected bool addPosition;
        [SerializeField] protected System.Collections.Generic.List<Vector3> listPoints;
        [SerializeField] protected Vector3[] pointArray;

        public Vector3[] Points { get => pointArray; }

        protected void AddPointsToList(Vector3[] points, System.Collections.Generic.List<Vector3> listToAdd, bool addPosition = false)
        {
            ArrayAndListExtensions.AddArrayToList(listToAdd, points);

            foreach (Vector3 point in points)
                if (listToAdd.Contains(point) == false)
                {
                        listToAdd.Add(point);
                }

            if (addPosition)
                UpdatePosition();
        }

        public Vector3[] UpdatePosition()
        {
            if (listPoints.Exists())
            {
                pointArray = new Vector3[listPoints.Count];
                for (int i = 0; i < listPoints.Count; i++)
                {
                    var pos = listPoints[i];
                    pointArray[i] = pos + Position;
                }

                return pointArray;
            }

            else
                return null;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            UpdatePosition();
        }
    }
}

