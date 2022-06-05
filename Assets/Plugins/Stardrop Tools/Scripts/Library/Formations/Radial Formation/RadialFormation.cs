
using UnityEngine;

namespace StardropTools.Formations
{
    public class RadialFormation : Formation
    {
        public enum RadialType { outline, filled, incrementOutline, incrementFilled }

        [Space]
        [SerializeField] RadialType radialType;
        [Space]
        [Range(0, 10)] [Min(0)] [SerializeField] int rings = 3;
        [Min(0)] [SerializeField] int startPoints = 8;
        [Min(0)] [SerializeField] int pointIncrement = 4;
        [Min(0)] [SerializeField] float radius = 1.75f;
        [Space]
        [Range(0, 128)] [Min(0)] [SerializeField] int pointCount = 0;
        [Range(0, 128)] [Min(0)] [SerializeField] int pointsOnRing = 0;
        [SerializeField] int nextRingPoints = 0;
        [SerializeField] int remainingPoints = 0;
        [Space]
        [SerializeField] float radialOcupation = 0;
        [SerializeField] System.Collections.Generic.List<Ring> ringArrayList;

        public void SetOutline()
        {
            radialType = RadialType.outline;
        }

        public void SetFilled()
        {
            radialType = RadialType.filled;
        }

        public void SetIncrementOutline()
        {
            radialType = RadialType.incrementOutline;
        }

        public void SetIncrementFilled(int points, float radius, bool generate = true)
        {
            radialType = RadialType.incrementFilled;
            this.radius = radius;

            pointCount = points;

            if (generate)
                GeneratePoints();
        }

        public void GeneratePoints()
        {
            // initialize list
            if (listPoints == null)
                listPoints = new System.Collections.Generic.List<Vector3>();
            listPoints.Clear();

            if (radialType == RadialType.outline)
                Outline();

            // loop through rings & create points
            if (radialType == RadialType.filled)
                Filled();

            if (radialType == RadialType.incrementOutline)
                IncrementOutline();

            if (radialType == RadialType.incrementFilled)
                IncrementFilled();
        }


        void Outline()
        {
            var points = VectorUtility.CreatePointCircleHorizontal(Vector3.zero, pointsOnRing, radius);
            AddPointsToList(points, listPoints);
        }


        // loop through rings & create points
        void Filled()
        {
            int pointCount = startPoints;

            for (int i = 0; i < rings; i++)
            {
                var points = VectorUtility.CreatePointCircleHorizontal(Vector3.zero, pointCount, radius * (i + 1));
                AddPointsToList(points, listPoints);

                pointCount += pointIncrement;
            }
        }

        void IncrementOutline()
        {
            radialOcupation = (float)pointsOnRing / startPoints;
            rings = MathUtility.GetIntegerFromFloat(radialOcupation);

            var ringPoints = VectorUtility.CreatePointCircleHorizontal(Vector3.zero, pointsOnRing, radius * (rings + 1));
            AddPointsToList(ringPoints, listPoints);
        }


        void IncrementFilled()
        {
            // reset values
            nextRingPoints = 0;
            remainingPoints = pointCount;        

            // Initialize / reset list of ring arrays
            ringArrayList = new System.Collections.Generic.List<Ring>();

            // create & populate rings
            int count = 0;
            while(remainingPoints > 0)
            {
                nextRingPoints = startPoints + pointIncrement * count;

                // create ring
                Ring ring = new Ring(nextRingPoints, false);
                ringArrayList.Add(ring);
                ring.id = count;

                // Populate ring
                while (remainingPoints > 0 && ring.FreePoints > 0)
                {
                    ring.AddSinglePoint();
                    remainingPoints--;
                }

                count++;
            }

            // create ring circles & 
            for (int i = 0; i < ringArrayList.Count; i++)
            {
                var ring = ringArrayList[i];
                Vector3[] ringPoints = VectorUtility.CreatePointCircle(Vector3.zero, new Vector3(90, 0, 90), ring.OccupiedPoints, radius * (i + 1));
                AddPointsToList(ringPoints, listPoints);
            }
            
            UpdatePosition();
        }



        protected override void OnValidate()
        {
            base.OnValidate();

            GeneratePoints();
        }
    }
}

