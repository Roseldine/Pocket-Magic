
using UnityEngine;

[System.Serializable]
public class GridData
{
    public enum PointDistribuition { areaForPoints, spaceBetweenPoints }
    public enum GridSpace { xz, xy, yz, xyz }

    [SerializeField] PointDistribuition distribuition;
    [SerializeField] GridSpace space = GridSpace.xz;
    [SerializeField] bool editable;
    [Space]
    [Min(0.001f)] [SerializeField] float width = 5;
    [Min(0.001f)] [SerializeField] float depth = 5;
    [Min(0.001f)] [SerializeField] float height;
    [Space]
    [Min(1)] [SerializeField] int unitWidth = 5;
    [Min(1)] [SerializeField] int unitDepth = 5;
    [Min(1)] [SerializeField] int unitHeight;
    [Space]
    public Transform targetTransform;
    [SerializeField] Vector3 startPosition;
    
    /// <summary>
    /// front left => front right => back right => back left
    /// </summary>
    [SerializeField] Vector3[] gridCorners;
    [SerializeField] Vector3[] gridPoints;

    public GridSpace Space { get => space; }
    public PointDistribuition Distribuition { get => distribuition; }

    public float Width { get => width; set { if (editable) width = value; } }
    public float Depth { get => depth; set { if (editable) depth = value; } }
    public float Height { get => height; set { if (editable) height = value; } }

    public int UnitWidth { get => unitWidth; set { if (editable) unitWidth = value; } }
    public int UnitDepth { get => unitDepth; set { if (editable) unitDepth = value; } }
    public int UnitHeight { get => unitHeight; set { if (editable) unitHeight = value; } }

    public Vector3 GridDimensions { get => new Vector3(width, height, depth); }
    public Vector3 GridUnityDimensions { get => new Vector3(unitWidth, unitHeight, unitDepth); }
    public Vector3 GridDimensionCenter { get => new Vector3(width, height, depth) / 2; }
    public Vector3 StartPosition { get => startPosition; }
    public Vector3 Rotation { get { if (targetTransform != null) return targetTransform.eulerAngles; else return Vector3.zero; } }
    public Vector3[] GridCorners { get => gridCorners; }
    public Vector3[] GridPoints { get => gridPoints; }

    /// <summary>
    /// Normalized 2D grid
    /// </summary>
    public GridData (float width, float depth, GridSpace space, PointDistribuition distribuition, bool isEditable = true, bool generatePoints = false)
    {
        this.distribuition = distribuition;
        this.space = space;

        this.width = width;
        this.depth = depth;
        height = 0;

        unitWidth = 1;
        unitDepth = 1;

        startPosition = Vector3.zero;

        editable = isEditable;

        if (generatePoints)
            GenerateGridPoints();
    }

    /// <summary>
    /// Normalized 3D grid
    /// </summary>
    public GridData(float width, float depth, float height, PointDistribuition distribuition, bool isEditable = false, bool generatePoints = false)
    {
        this.distribuition = distribuition;
        space = GridSpace.xyz;

        this.width = width;
        this.depth = depth;
        this.height = height;

        unitWidth = 1;
        unitDepth = 1;
        unitHeight = 1;

        editable = isEditable;

        startPosition = Vector3.zero;

        if (generatePoints)
            GenerateGridPoints();
    }


    /// <summary>
    /// Adjustable 2D grid
    /// units determain how many points are in between width & depth
    /// </summary>
    public GridData(Vector3 startPosition, float width, float depth, int unitWidth, int unitDepth, GridSpace space, PointDistribuition distribuition, bool isEditable = false, bool generatePoints = false)
    {
        this.distribuition = distribuition;
        this.space = space;

        this.width = width;
        this.depth = depth;
        height = 0;

        this.unitWidth = unitWidth;
        this.unitDepth = unitDepth;

        this.startPosition = startPosition;

        if (generatePoints)
            GenerateGridPoints();
    }


    /// <summary>
    /// Adjustable 3D grid
    /// units determain how many points are in between width & depth
    /// </summary>
    public GridData(Vector3 startPosition, float width, float depth, float height, int unitWidth, int unitDepth, int unitHeight, PointDistribuition distribuition, bool isEditable = false, bool generatePoints = false)
    {
        this.distribuition = distribuition;
        space = GridSpace.xyz;

        this.width = width;
        this.depth = depth;
        this.height = height;

        this.unitWidth = unitWidth;
        this.unitDepth = unitDepth;
        this.unitHeight = unitHeight;

        editable = isEditable;

        this.startPosition = startPosition;

        if (generatePoints)
            GenerateGridPoints();
    }

    public Vector3[] GenerateGridPoints()
    {
        CheckGridSpace();

        gridPoints = GridGenerator.GenerateGrid(this);
        CalculateCorners();
        return gridPoints;
    }
    
    public Vector3[] GenerateGridPoints(Vector3 startPoint)
    {
        CheckGridSpace();
        startPosition = startPoint;

        gridPoints = GridGenerator.GenerateGrid(this);
        CalculateCorners();

        return gridPoints;
    }

    void CheckGridSpace()
    {
        if (space == GridSpace.xy)
        {
            if (width == 0)
                width = 1;

            if (depth != 0)
                depth = 0;

            if (height == 0)
                height = 1;
        }

        else if (space == GridSpace.xz)
        {
            if (width == 0)
                width = 1;

            if (depth == 0)
                depth = 1;

            if (height != 0)
                height = 0;
        }

        else if (space == GridSpace.yz)
        {
            if (width != 0)
                width = 0;

            if (depth == 0)
                depth = 1;

            if (height == 0)
                height = 1;
        }

        if (space == GridSpace.xyz)
        {
            if (depth == 0)
                depth = 1;

            if (height == 0)
                height = 1;

            if (depth == 0)
                depth = 1;
        }
    }

    Vector3[] CalculateCorners()
    {
        // grid is XY or XZ
        if (space != GridSpace.xyz)
        {
            float size = 0;
            Vector3 axisVector = Vector3.zero;
            int unitySize = 0;

            if (space == GridSpace.xy)
            {
                size = height;
                unitySize = unitWidth;
                axisVector = Vector3.up;
            }
            else if (space == GridSpace.xz)
            {
                size = depth;
                unitySize = unitWidth;
                axisVector = Vector3.forward;
            }
            else if (space == GridSpace.yz)
            {
                size = depth;
                unitySize = UnitDepth;
                axisVector = Vector3.forward;
            }

            gridCorners = new Vector3[4];

            gridCorners[0] = gridPoints[0]; // front left
            gridCorners[1] = gridPoints[unitySize - 1]; // front right
            gridCorners[2] = gridCorners[1] + axisVector * size; // back right
            gridCorners[3] = gridCorners[0] + axisVector * size; // back left
        }

        // grid is XYZ
        else
        {
            gridCorners = new Vector3[8];

            // lower part
            gridCorners[0] = gridPoints[0]; // front left
            gridCorners[1] = gridPoints[unitWidth - 1]; // front right
            gridCorners[2] = gridCorners[1] + Vector3.forward * depth; // back right
            gridCorners[3] = gridCorners[0] + Vector3.forward * depth; // back left

            // higher part
            gridCorners[4] = gridCorners[0] + Vector3.up * height; // front left
            gridCorners[5] = gridCorners[1] + Vector3.up * height; // front right
            gridCorners[6] = gridCorners[2] + Vector3.up * height; // back right
            gridCorners[7] = gridCorners[3] + Vector3.up * height; // back right
        }

        return gridCorners;
    }
}