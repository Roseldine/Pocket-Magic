
[System.Serializable]
public class ESpell
{
    public enum MageLevel { trainee, aprentice, adept, advanced, expert, master, grandMaster, elder, deity }
    public enum SpellTier { basic, advanced, elite, ultimate }
    public enum SpellNature { damage, shield, heal }
    public enum SpellType { projectile, beam, spray, cleave, slash, blast, wall, puddle, fissure, shield }
    public enum SpellSpeed { slow, normal, fast, overtime, instant }
    public enum SpellTarget { opponent, self, all }
    public enum SpellCastOrigin { weapon, self }

    public enum ProjectileType { line, arc, hoaming }

    public enum SpellProperty { none, shieldBreak, burn, stun, slowCast }
    public enum SpellOpponentRelation { none, moveToTargetDirect, moveToTargetGround, onTopDirect, onTopGround }

}