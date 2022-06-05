using StardropTools.UI;
using UnityEngine;

public class UIMenuFinish : StardropTools.UI.UIMenu
{
    [SerializeField] StardropTools.Tween.TweenCluster openTweens;
    [SerializeField] TMPro.TextMeshProUGUI textPause;
    [SerializeField] UIButton[] continueButtons;

    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < continueButtons.Length; i++)
            continueButtons[i].Initialize();
    }

    public override void Open()
    {
        base.Open();
        openTweens.PlayTweens();
    }
}