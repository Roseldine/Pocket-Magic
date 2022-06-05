using System.Collections.Generic;
using UnityEngine;
using StardropTools;

public class EnvironmentManager : StardropTools.Singletons.SingletonCoreManager<EnvironmentManager>
{
    [SerializeField] Environment environment;

    public Environment Environment { get => environment; }

    public override void Initialize()
    {
        base.Initialize();
        SubscribeToEvents();
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
    }

    public Vector3 GetTargetPosition(Vector3 position)
    {
        // is player so needs opponent direction
        if (position.z < 0)
            return Environment.OpponentTargetPos;

        // is opponent so needs player pos
        else
            return Environment.PlayerTargetPos;
    }

    protected override void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}