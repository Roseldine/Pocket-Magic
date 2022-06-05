using System.Collections;
using UnityEngine;
using StardropTools;

public class Player : Agent
{
    SpellIdentifier spellIdentifier;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeToEvents();
    }


    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        SpellScene.OnSpellCastRelease.AddListener(CheckForSpell);
    }

    private void Update()
    {
        UpdateAgent();
    }

    public virtual void UpdateAgent()
    {
        
    }

    protected override void AnimEvent(int eventID)
    {
        if (eventID == 0)
            SpawnSpell(spellIdentifier);
    }

    void CheckForSpell(int[] spellPoints)
    {
        spellIdentifier = SpellManager.Instance.ValidateSpell(spellPoints);

        if (spellIdentifier == null)
            return;

        PlayRandomSpell();
    }

    public void SpawnSpell()
    {
        spell = SpawnSpell(spellIdentifier);
    }
}