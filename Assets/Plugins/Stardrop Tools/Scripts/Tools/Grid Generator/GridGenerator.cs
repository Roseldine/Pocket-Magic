
using UnityEngine;

public static class GridGenerator
{
    public static Vector3[] GenerateGrid(GridData data)
    {
        /*
        if (data.Width == 0 || data.Depth == 0 || data.Height == 0)
            Debug.Log("One side has 0 (zero) length");

        if (data.UnitWidth == 0 || data.UnitDepth == 0 || data.UnitHeight == 0)
            Debug.Log("One axis has 0 (zero) units");
        */

        // get generic info
        GridData.PointDistribuition distribuition = data.Distribuition;
        GridData.GridSpace space = data.Space;
        Vector3 startPosition = data.StartPosition;
        System.Collections.Generic.List<Vector3> pointList = new System.Collections.Generic.List<Vector3>();

        Matrix4x4 rotMatrix = Matrix4x4.Rotate(Quaternion.Euler(data.Rotation));
        Vector3 point = startPosition;

        if (distribuition == GridData.PointDistribuition.areaForPoints)
        {
            float incrementWidth = data.Width / (data.UnitWidth - 1);
            float incrementDepth = data.Depth / (data.UnitDepth - 1);
            float incrementHeight = data.Height / (data.UnitHeight - 1);

            if (space == GridData.GridSpace.xy)
            {
                for (int y = 0; y < data.UnitHeight; y++)
                {
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * incrementWidth, y * incrementHeight, 0);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }

            else if (space == GridData.GridSpace.xz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * incrementWidth, 0, z * incrementDepth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }

            else if (space == GridData.GridSpace.yz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int y = 0; y < data.UnitWidth; y++)
                    {
                        point = new Vector3(0, y * incrementHeight, z * incrementDepth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }

            else if (space == GridData.GridSpace.xyz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int y = 0; y < data.UnitHeight; y++)
                        for (int x = 0; x < data.UnitWidth; x++)
                        {
                            point = new Vector3(x * incrementWidth, y * incrementHeight, z * incrementDepth);
                            point = rotMatrix.MultiplyPoint3x4(point);
                            pointList.Add(point + startPosition);
                        }
                }
            }
        }

        if (distribuition == GridData.PointDistribuition.spaceBetweenPoints)
        {
            Vector3 vecX = Vector3.right * data.Width;
            Vector3 vecY = Vector3.up * data.Height;
            Vector3 vecZ = Vector3.forward * data.Depth;

            float width = data.Width;
            float height = data.Height;
            float depth = data.Depth;

            Vector3[] prevPoints = new Vector3[data.UnitWidth];

            if (space == GridData.GridSpace.xy)
            {
                for (int y = 0; y < data.UnitHeight; y++)
                { 
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * width, y * height, 0); point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                        prevPoints[x] = point;
                    }
                }
            }

            else if (space == GridData.GridSpace.xz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * width, 0, z * depth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                        prevPoints[x] = point;
                    }
                }
            }

            else if (space == GridData.GridSpace.yz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int y = 0; y < data.UnitWidth; y++)
                    {
                        point = new Vector3(0, y * height, z * depth); point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                        prevPoints[y] = point;
                    }
                }
            }

            else if (space == GridData.GridSpace.xyz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int y = 0; y < data.UnitHeight; y++)
                        for (int x = 0; x < data.UnitWidth; x++)
                        {
                            point = new Vector3(x * width, y * height, z * depth); point = rotMatrix.MultiplyPoint3x4(point);
                            pointList.Add(point + startPosition);
                            prevPoints[x] = point;
                        }
                }
            }
        }


        return pointList.ToArray();
    }

    /*
    public static Vector3[] GenerateGrid(GridData data, Vector3 targetRotation)
    {
        Quaternion rotation = Quaternion.Euler(targetRotation);
        Matrix4x4 rotMatrix = Matrix4x4.Rotate(rotation);

        // get generic info
        GridData.GridSpace space = data.Space;
        Vector3 startPosition = data.StartPosition;
        System.Collections.Generic.List<Vector3> pointList = new System.Collections.Generic.List<Vector3>();

        float incrementWidth = data.Width / (data.UnitWidth - 1);
        float incrementDepth = data.Depth / (data.UnitDepth - 1);
        float incrementHeight = data.Height / (data.UnitHeight - 1);

        Vector3 point = startPosition;

        if (space != GridData.GridSpace.xyz)
        {
            if (space == GridData.GridSpace.xy)
            {
                for (int y = 0; y < data.UnitHeight; y++)
                {
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * incrementWidth, y * incrementHeight, 0);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }

            else if (space == GridData.GridSpace.xz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * incrementWidth, 0, z * incrementDepth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }

            else if (space == GridData.GridSpace.yz)
            {
                for (int z = 0; z < data.UnitDepth; z++)
                {
                    for (int y = 0; y < data.UnitWidth; y++)
                    {
                        point = new Vector3(0, y * incrementHeight, z * incrementDepth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
                }
            }
        }

        else if (space == GridData.GridSpace.xyz)
        {
            for (int z = 0; z < data.UnitDepth; z++)
            {
                for (int y = 0; y < data.UnitHeight; y++)
                    for (int x = 0; x < data.UnitWidth; x++)
                    {
                        point = new Vector3(x * incrementWidth, y * incrementHeight, z * incrementDepth);
                        point = rotMatrix.MultiplyPoint3x4(point);
                        pointList.Add(point + startPosition);
                    }
            }
        }


        return pointList.ToArray();
    }
    */
    public static Vector3 GetRandomPoint(Vector3[] gridPoints)
    {
        int randInt = Random.Range(0, gridPoints.Length);
        return gridPoints[randInt];
    }

    public static Vector3[] GetRandomPoints(Transform gridTransform, Vector3[] gridPoints, int amount, float division = 2)
    {
        if (gridPoints.Length <= amount)
            amount = Mathf.RoundToInt(gridPoints.Length / division);

        System.Collections.Generic.List<Vector3> randomPointList = new System.Collections.Generic.List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            Vector3 randPoint = GetRandomPoint(gridPoints);

            while (randomPointList.Contains(randPoint))
                randPoint = GetRandomPoint(gridPoints);

            randomPointList.Add(randPoint + gridTransform.position);
        }

        return randomPointList.ToArray();
    }
}
