using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;
using StardropTools.Tween;

public class SpellCard : CoreObject
{

    [Header("Card Components")]
    [SerializeField] LineRenderer line;
    [SerializeField] RectTransform parentPoints;
    [SerializeField] SpellCardPoint[] points;
    [Space]
    [SerializeField] TweenCluster clusterNewCard;
    [SerializeField] TweenCluster clusterUseCard;

    [Header("Point Properties")]
    [SerializeField] float radius = 2f;
    [SerializeField] bool validateRadius;
    [SerializeField] bool getPoints;

    public override void Initialize()
    {
        base.Initialize();
        clusterNewCard.Initialize();
        clusterUseCard.Initialize();
    }

    public void SetPoints(int[] points)
    {

    }

    protected override void OnValidate()
    {
        base.OnValidate();

        if (getPoints)
        {
            points = Utilities.GetItems<SpellCardPoint>(parentPoints);
            getPoints = false;
        }

        if (validateRadius && parentPoints != null && points.Length == 6)
        {
            var positions = VectorUtility.CreatePointCircle(parentPoints.position, Vector3.forward * 90, points.Length, radius);
            
            for (int i = 0; i < points.Length; i++)
            {
                points[i].pointID = i;
                points[i].Position = positions[i];
            }
        }
    }
}
