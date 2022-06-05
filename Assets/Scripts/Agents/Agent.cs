
using UnityEngine;
using StardropTools;

public class Agent : CoreObject
{
    [SerializeField] protected TeamSO team;
    [SerializeField] protected new SimpleAnimation animation;
    [SerializeField] protected Damageable damageable;
    [Space]
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected Transform opponentTarget;

    protected Spell spell;
    public Team Team { get => team.Team; }

    public override void Initialize()
    {
        base.Initialize();

        GetTargetPoint();
        animation.OnAnimEventInt.AddListener(AnimEvent);
    }

    protected virtual void AnimEvent(int eventID)
    {

    }

    protected virtual void SpellHit(Spell spell, Collider collider)
    {
        //Debug.Log(collider.name);
        //Agent agent = collider.GetComponent<Agent>();
        //
        //if (agent.Team.Index != Team.Index)
        //    damageable.ApplyDamage(spell.Damage);
    }

    protected void GetTargetPoint()
    {
        if (PosZ < 0)
            opponentTarget = EnvironmentManager.Instance.Environment.opponentTargetPoint;
        else
            opponentTarget = EnvironmentManager.Instance.Environment.playerTargetPoint;
    }

    protected Spell SpawnSpell(SpellIdentifier spellIdentifier)
    {
        if (spellIdentifier == null)
            return null;

        Vector3 targetPos = EnvironmentManager.Instance.GetTargetPosition(Position);
        Vector3 direction = projectileSpawnPoint.position - targetPos;
        Quaternion rotation = Quaternion.LookRotation(direction);

        var spell = SpellManager.Instance.SpawnSpell(spellIdentifier, projectileSpawnPoint.position, rotation);
        spell.sourceAgent = this;
        (spell as SpellProjectile).SetPoints(projectileSpawnPoint.position, targetPos);

        spell.OnSpellHit.AddListener(SpellHit);

        return spell;
    }

    public void SetAnimTrigger(int animID)
    {
        animation.SetAnimationTrigger(animID, false);
    }

    public void PlayAnimation(int animID, bool forceAnim = false)
        => animation.PlayAnimation(animID, forceAnim);

    public void CrossFadeAnimation(int animID, bool forceAnim = false)
        => animation.CrossFadeAnimation(animID, forceAnim);


    public void PlayIdle()
        => SetAnimTrigger(0);

    public void PlayRun()
        => SetAnimTrigger(1);

    public void PlayRandomSpell()
        => SetAnimTrigger(Random.Range(2, 5));
    public void PlaySpellOne()
        => SetAnimTrigger(2);
    public void PlaySpellTwo()
        => SetAnimTrigger(3);
    public void PlaySpellThree()
        => SetAnimTrigger(4);

    public void PlaySpellStart()
        => SetAnimTrigger(5);
    public void PlaySpellLoop()
        => CrossFadeAnimation(6);
}
