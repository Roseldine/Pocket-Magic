using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpellProjectile : Spell
{
    [SerializeField] protected Vector3 startPos;
    [SerializeField] protected Vector3 targetPos;
    [Space]
    [SerializeField] protected float speed;

    Vector3 direction;

    public virtual void SetPoints(Vector3 start, Vector3 target)
    {
        startPos = start;
        targetPos = target;

        direction = target - start;
    }

    public override void UpdateSpell()
    {
        Transform.Translate(Transform.forward * speed * Time.deltaTime);
        hitScan.Scan();
    }
}
