using StardropTools.UI;
using UnityEngine;

public class UIMenuPause : StardropTools.UI.UIMenu
{
    [SerializeField] StardropTools.Tween.TweenCluster openTweens;
    [SerializeField] TMPro.TextMeshProUGUI textPoints;
    [SerializeField] UIButton reviveButton;
    [SerializeField] UIButton[] continueButtons;

    public override void Initialize()
    {
        base.Initialize();

        if (reviveButton != null)
            reviveButton.Initialize();

        if (continueButtons.Exists())
            for (int i = 0; i < continueButtons.Length; i++)
                continueButtons[i].Initialize();
    }

    public override void Open()
    {
        base.Open();
        openTweens.PlayTweens();
    }

    public void SetPoints(int value)
        => textPoints.text = value.ToString();
}