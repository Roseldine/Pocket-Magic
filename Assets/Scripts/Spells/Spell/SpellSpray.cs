using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpray : Spell
{
    [SerializeField] protected Vector3 startPos;
    [SerializeField] protected Vector3 targetPos;
    [Space]
    [SerializeField] protected float distance;
    [SerializeField] protected int damagePerSecond = 1;

    Vector3 direction;

    public virtual void SetPoints(Vector3 start, Vector3 target)
    {
        startPos = start;
        targetPos = target;

        direction = target - start;
    }

    public override void UpdateSpell()
    {
        Debug.Log("Spray is updating");
    }
}
