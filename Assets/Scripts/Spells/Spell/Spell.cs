using System.Collections;
using UnityEngine;
using StardropTools;

public abstract class Spell : CoreObject
{
    public Agent sourceAgent;
    [SerializeField] protected int damage;
    [SerializeField] protected OverlapBoxScanner hitScan;
    [SerializeField] protected TrailRenderer[] trails;

    public int Damage { get => damage; }

    public CoreEvent<Collider> OnHit { get => hitScan.OnEnter; }
    public readonly CoreEvent<Spell, Collider> OnSpellHit = new CoreEvent<Spell, Collider>();

    public override void Initialize()
    {
        base.Initialize();
        //OnHit.AddListener(SpellHit);
        hitScan.OnEnter.AddListener(SpellHit);
    }

    public abstract void UpdateSpell();

    protected virtual void SpellHit(Collider collider)
    {
        if (collider == null)
            Debug.Log("Collider is null!");

        //OnSpellHit?.Invoke(this, collider);
        //OnSpellHit.RemoveAllListeners();
        //
        //Debug.Log(collider.name);
        //
        //Despawn();
    }

    protected void ResetTrails()
    {
        if (trails != null && trails.Length > 0)
            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].Clear();
                Debug.Log("Cleared!");
            }

        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke(nameof(ResetTrails), .01f);
        //ResetTrails();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ResetTrails();
    }
}