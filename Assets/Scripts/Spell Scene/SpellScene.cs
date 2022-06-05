using System.Collections.Generic;
using UnityEngine;
using StardropTools.Pool;

public class SpellScene : StardropTools.Singletons.SingletonCoreManager<SpellScene>
{
    [Header("Spell Raycast")]
    [SerializeField] new Camera camera;
    [SerializeField] LineRenderer line;
    [SerializeField] Transform parentPoints;
    [SerializeField] SpellPoint[] spellPoints;
    [SerializeField] bool getPoints;

    [Header("Point detection")]
    [SerializeField] LayerMask layer;
    [SerializeField] List<SpellPoint> selectedPoints;
    [SerializeField] bool removePreviousPoint;

    public SpellPoint LastSelectedPoint { get; private set; }
    int[] pointIds;


    [Header("Point configuration")]
    [SerializeField] float pointScale = .5f;
    [SerializeField] float pointRadius = 2f;
    [SerializeField] float pointDistance = 3f;
    [SerializeField] float pointHeight = -3f;
    [SerializeField] Vector3 pointCircleRotation = Vector3.right * 90;

    public static readonly CoreEvent OnSpellPointAdd = new CoreEvent();
    public static readonly CoreEvent OnSpellPointRemoved = new CoreEvent();
    public static readonly CoreEvent<int[]> OnSpellCastRelease = new CoreEvent<int[]>();

    public override void Initialize()
    {
        base.Initialize();
        SubscribeToEvents();

        selectedPoints = new List<SpellPoint>();
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        SingleInputManager.OnInput.AddListener(UpdateSpellLineLastPoint);
        SingleInputManager.OnInputEnd.AddListener(ValidateSpell);
    }

    

    public void AddSelectedPoint(SpellPoint point)
    {
        if (selectedPoints.Count > 0 && point.pointID == LastSelectedPoint.pointID)
        {
            OnSpellPointRemoved?.Invoke();

            if (removePreviousPoint)
            {
                selectedPoints.Remove(point);
                RefreshSpellLine();
            }

            Debug.Log("Can't add the same point twice");
            return;
        }

        selectedPoints.Add(point);
        LastSelectedPoint = selectedPoints[Mathf.Clamp(selectedPoints.Count - 1, 0, selectedPoints.Count)];
        RefreshSpellLine();

        OnSpellPointAdd?.Invoke();
        //Debug.Log("Added point!");
    }

    void RefreshSpellLine()
    {
        line.positionCount = selectedPoints.Count + 1;

        for (int i = 0; i < selectedPoints.Count; i++)
            line.SetPosition(i, selectedPoints[i].Position);
    }

    void UpdateSpellLineLastPoint()
    {
        if (selectedPoints.Count < 1)
            return;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 100, layer))
            line.SetPosition(line.positionCount - 1, hit.point);

        //Debug.Log("Updating last point");
    }

    void ValidateSpell()
    {
        // send spell points for id
        pointIds = new int[selectedPoints.Count];
        for (int i = 0; i < pointIds.Length; i++)
            pointIds[i] = selectedPoints[i].pointID;

        OnSpellCastRelease?.Invoke(pointIds);

        line.positionCount = 0;
        selectedPoints.Clear();
        LastSelectedPoint = null;
    }

    void RefreshPointsPosition()
    {
        if (spellPoints.Length == 0)
            return;

        Vector3 position = Vector3.forward * pointDistance + Vector3.right * pointHeight;
        var points = VectorUtility.CreatePointCircle(position, pointCircleRotation, spellPoints.Length, pointRadius);
        for (int i = 0; i < spellPoints.Length; i++)
        {
            spellPoints[i].pointID = i;
            spellPoints[i].Position = points[i];
            spellPoints[i].Scale = Vector3.one * pointScale;
        }
    }

    

    protected override void OnValidate()
    {
        base.OnValidate();

        if (spellPoints.Length > 0)
            RefreshPointsPosition();

        if (getPoints && parentPoints != null)
        {
            spellPoints = Utilities.GetItems<SpellPoint>(parentPoints);
            getPoints = false;
        }
    }
}
