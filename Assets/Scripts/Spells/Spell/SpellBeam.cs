using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;
using Cysharp.Threading.Tasks;

public class SpellBeam : SpellProjectile
{
    [SerializeField] float duration = 2;

    [Header("Beam Geometry")]
    [SerializeField] Transform hitParticles;
    [SerializeField] LineRenderer line;
    [SerializeField] SimpleAnimationTransitor animTransitor;

    float time, percent;
    UniTaskVoid spellTaskCR, spellDamageCR;

    public override void SetPoints(Vector3 start, Vector3 target)
    {
        base.SetPoints(start, target);
        spellTaskCR = SpellTask();
    }

    private async UniTaskVoid SpellTask()
    {
        Vector3 lerped = Vector3.zero;
        time = 0;
        percent = 0;

        line.positionCount = 2;

        // time to reach spell
        while (time < speed)
        {
            percent = time / duration;
            lerped = Vector3.Lerp(startPos, targetPos, percent);
            line.SetPosition(1, lerped);
            hitParticles.position = lerped;

            time += Time.deltaTime;
            await UniTask.Yield();
        }

        line.SetPosition(1, targetPos);
        hitParticles.position = targetPos;

        time = 0;
        float damageTimer = 0;

        

        // spell duration
        while (time < speed)
        {
            

            time += Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
