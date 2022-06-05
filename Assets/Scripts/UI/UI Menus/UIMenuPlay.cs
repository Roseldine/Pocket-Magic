using System.Collections;
using UnityEngine;
using StardropTools;

public class UIMenuPlay : StardropTools.UI.UIMenu
{
    [SerializeField] StardropTools.Tween.TweenCluster openTweenCluster;
    [SerializeField] HealthContainer healthContainer;
    [SerializeField] TMPro.TextMeshProUGUI textPoints;
    [SerializeField] TMPro.TextMeshProUGUI textDistance;
    [SerializeField] StardropTools.Tween.TweenComponentLocalScale tweenPointScale;

    public override void Initialize()
    {
        base.Initialize();

        if (healthContainer != null)
            healthContainer.Initialize();

        GameManager.Instance.OnIdleEnter.AddListener(() => SetPoints(0));
        GameManager.Instance.OnIdleEnter.AddListener(() => SetDistance(0));

        UIManager.OnUpdatePoints.AddListener(SetPoints);
        UIManager.OnUpdateDistance.AddListener(SetDistance);
    }

    public override void Open()
    {
        base.Open();

        if (openTweenCluster != null)
            openTweenCluster.PlayTweens();
    }

    public void SetPoints(int value)
    {
        if (textPoints == null)
            return;

        textPoints.text = value.ToString();
        tweenPointScale.PlayTween();
    }

    public void SetDistance(int value)
    {
        if (textDistance == null)
            return;

        textDistance.text = value.ToString();
    }
}