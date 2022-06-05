
namespace StardropTools.Formations
{
    [System.Serializable]
    public class Ring
    {
        public int id;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool isFull;
        [UnityEngine.SerializeField] int occupiedPoints;
        [UnityEngine.SerializeField] int freePoints;
        [UnityEngine.SerializeField] bool[] pointArray;

        public int OccupiedPoints { get => occupiedPoints; }
        public int FreePoints { get => freePoints = length - occupiedPoints; }
        public int length { get => pointArray.Length; }
        public bool IsFull { get => isFull; }

        static bool free = false;
        static bool occupied = true;

        public Ring() { }
        public Ring(int pointCount, bool full = false)
        {
            pointArray = new bool[pointCount];

            if (full)
            {
                for (int i = 0; i < pointArray.Length; i++)
                {
                    pointArray[i] = occupied;
                    occupiedPoints++;
                }

                isFull = true;
            }

            else
            {
                for (int i = 0; i < pointArray.Length; i++)
                    pointArray[i] = free;
            }
        }

        public Ring(int pointCount, int occupation = 0)
        {
            pointArray = new bool[pointCount];

            for (int i = 0; i < pointArray.Length; i++)
                pointArray[i] = free;

            if (occupation > 0)
            {
                for (int i = 0; i < occupation; i++)
                {
                    pointArray[i] = occupied;
                    occupiedPoints++;

                    // reached end, array is full
                    if (i == pointArray.Length - 1)
                        isFull = true;
                }
            }
        }


        /// <summary>
        /// Remove points from array
        /// </summary>
        /// <returns> free points </returns>
        public int AddSinglePoint()
        {
            if (isFull)
                return 0;

            for (int i = 0; i < pointArray.Length; i++)
            {
                if (pointArray[i] == free)
                {
                    pointArray[i] = occupied;

                    occupiedPoints = i + 1;
                    freePoints = length - occupiedPoints;
                    break;
                }
            }

            if (freePoints == 0)
                isFull = true;
            else
                isFull = false;

            return freePoints;
        }

        /// <summary>
        /// Remove points from array
        /// </summary>
        /// <returns> free points </returns>
        public int RemoveSinglePoint()
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (pointArray[i] == occupied)
                {
                    pointArray[i] = free;

                    freePoints = length - 1;
                    break;
                }
            }

            if (freePoints == 0)
                isFull = true;
            else
                isFull = false;

            return freePoints;
        }
    }
}