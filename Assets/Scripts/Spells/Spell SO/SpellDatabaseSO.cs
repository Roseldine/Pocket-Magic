using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Pocket Magic / Spell Database")]
public class SpellDatabaseSO : ScriptableObject
{
    [SerializeField] bool reverse;
    [Tooltip("0-basic, 1-advanced, 2-elite, 3-ultimate")]
    [SerializeField] SpellDatabase[] spellDatabases;

    public int SpellDBCount { get => spellDatabases.Length; }
    public SpellDatabase GetDatabaseByIndex(int index)
        => spellDatabases[index];


    public SpellIdentifier ValidateSpell(int[] spellPoints)
    {
        if (spellPoints.Length < 3)
            return null;

        // find database based on point length
        ESpell.SpellTier tier = ESpell.SpellTier.basic;

        if (spellPoints.Length == 4)
            tier = ESpell.SpellTier.advanced;
        else if (spellPoints.Length == 5)
            tier = ESpell.SpellTier.elite;
        else if (spellPoints.Length == 6)
            tier = ESpell.SpellTier.ultimate;

        // check to see if database exists
        var db = spellDatabases[(int)tier];
        var spell = db.GetSpellByPoints(spellPoints);

        if (spell != null)
            return new SpellIdentifier(tier, (int)tier, spell.SpellID);
        else
        {
            Debug.Log("Spell not found...");
            return null;
        }
    }



    private void OnValidate()
    {
        if (reverse && spellDatabases.Length > 0)
        {
            for (int i = 0; i < spellDatabases.Length; i++)
                spellDatabases[i].Reverse();

            reverse = false;
        }
    }
}

[System.Serializable]
public class SpellDatabase
{
    [SerializeField] string databaseName;
    [SerializeField] ESpell.MageLevel spellTier;
    [SerializeField] SpellSO[] spells;

    public int SpellCount { get => spells.Length; }
    public ESpell.MageLevel SpellTier { get => spellTier; }

    public SpellSO GetSpellByIndex(int index)
        => spells[index];

    public SpellSO GetSpellByPoints(int[] points)
    {
        SpellSO spell = null;

        for (int i = 0; i < spells.Length; i++)
        {
            var s = spells[i];
            if (s.CheckIfIsSameSpell(points))
            {
                spell = s;
                break;
            }
        }

        if (spell == null)
            Debug.Log("No such spell was found...");

        return spell;
    }

    public void Reverse()
    {
        databaseName = spellTier.ToString();

        for (int i = 0; i < spells.Length; i++)
        {
            spells[i].SpellID = (int)spellTier + i;
            spells[i].spellTier = spellTier;
            spells[i].ReversePoints();
        }
    }
}

[System.Serializable]
public class SpellIdentifier
{
    public ESpell.SpellTier SpellTier;
    public int PoolClusterIdentifier;
    public int SpellIndex;

    public SpellIdentifier() { }
    public SpellIdentifier(ESpell.SpellTier SpellTier, int PoolClusterIdentifier, int SpellIndex)
    {
        this.SpellTier = SpellTier;
        this.PoolClusterIdentifier = PoolClusterIdentifier;
        this.SpellIndex = SpellIndex;
    }

    //public ESpell.SpellTier SpellTier { get; private set; }
    //public int PoolIdentifier { get; private set; }
    //public int SpellIndex { get; private set; }
}