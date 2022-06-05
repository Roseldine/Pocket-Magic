using UnityEngine;

[System.Serializable]
public class SpellData
{
    public Sprite icon;
    [SerializeField] ESpell.MageLevel spellTier;
    [SerializeField] int spellID;
    [SerializeField] string spellName;
    [SerializeField] int[] spellPoints;
    [SerializeField] int[] reversedSpellPoints;

    public ESpell.MageLevel SpellTier { get => spellTier; }
    public int SpellID { get => spellID; }
    public string SpellName { get => spellName; }
    public Sprite SpellIcon { get => icon; }
    public int[] SpellPoints { get => spellPoints; }

    public SpellData(int spellID, string spellName, ESpell.MageLevel spellTier, int[] spellPoints)
    {
        this.spellID = spellID;
        this.spellName = spellName;
        this.spellPoints = spellPoints;
        this.spellTier = spellTier;
    }

    public bool CheckIfPointsBelong(int[] points)
    {
        bool value = true;

        if (spellPoints[0] == points[0])
            for (int i = 0; i < spellPoints.Length; i++)
            {
                if (spellPoints[i] != points[i])
                {
                    value = false;
                    break;
                }
            }

        // reversed
        else
        {
            if (spellPoints[spellPoints.Length - 1] == points[points.Length - 1])
                for (int i = 0; i < reversedSpellPoints.Length; i++)
                {
                    if (reversedSpellPoints[i] != points[i])
                    {
                        value = false;
                        break;
                    }
                }
        }
        

        return value;
    }

    public void CalculateReversed()
    {
        reversedSpellPoints = Utilities.ReverseArray(spellPoints);
    }
}
