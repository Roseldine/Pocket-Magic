using System.Collections;
using UnityEngine;
using StardropTools;

public class SpellPoint : CoreObject
{
    public int pointID;
    public readonly CoreEvent OnSpellSelected = new CoreEvent();

    protected override void Awake()
    {
        base.Awake();
        OnSpellSelected.AddListener(AddPoint);
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
            OnSpellSelected?.Invoke();
    }

    void AddPoint()
    {
        SpellScene.Instance.AddSelectedPoint(this);
    }
}