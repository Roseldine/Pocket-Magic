using System.Collections;
using UnityEngine;
using StardropTools;
using StardropTools.UI;

public class UIMenuIdle : StardropTools.UI.UIMenu
{
    [SerializeField] UIButton playButton;

    public override void Initialize()
    {
        base.Initialize();

        playButton.Initialize();
        playButton.OnClick.AddListener(() => GameManager.Instance.ChangeState(2));
    }
}