
using UnityEngine;

[CreateAssetMenu(menuName = "Pocket Magic / Spell")]
public class SpellSO : ScriptableObject
{
    public ESpell.MageLevel spellTier;
    [SerializeField] string spellName;
    [SerializeField] int spellID;
    [SerializeField] int damage;
    [SerializeField] GameObject spellPrefab;
    [SerializeField] int[] spellPoints;
    [SerializeField] int[] reversedSpellPoints;
    [SerializeField] bool reverse;

    public GameObject SpellPrefab { get => spellPrefab; }
    public int SpellID { get => spellID; set => spellID = value; }
    public int Damage { get => damage; }
    public int[] SpellPoints { get => spellPoints; }
    public int[] ReversedSpellPoints { get => reversedSpellPoints; }

    public void ReversePoints()
        => reversedSpellPoints = Utilities.ReverseArray(spellPoints);

    public bool CheckIfIsSameSpell(int[] points)
    {
        bool isEqual = true;

        // only evaluate if same length & same start point
        if (points.Length == spellPoints.Length)
        {
            // normal direction
            if (points[0] == spellPoints[0])
            {
                for (int i = 0; i < points.Length; i++)
                    if (points[i] != spellPoints[i])
                    {
                        isEqual = false;
                        break;
                    }
            }

            // reversed direction
            isEqual = true;
            int[] reversed = Utilities.ReverseArray(points);
            if (reversed[0] == reversedSpellPoints[0])
            {
                for (int i = 0; i < reversed.Length; i++)
                    if (reversed[i] != reversedSpellPoints[i])
                    {
                        isEqual = false;
                        break;
                    }
            }
        }

        else
            isEqual = false;

        return isEqual;
    }

    private void OnValidate()
    {
        if (reverse)
        {
            ReversePoints();
            reverse = false;
        }
    }
}