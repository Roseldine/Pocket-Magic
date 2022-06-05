
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class Heal
    {
        public enum EHealReason { Unkown, Combat, FriendlyFire, Map, NPC}
        public enum EHealType { Value, PercentMaxHealth, PercentRemainHealth }

        [SerializeField] EHealReason healReason;
        [SerializeField] EHealType healType;
        [SerializeField] int healAmount;
        [SerializeField] float healPercentage;

        public EHealReason HealReason { get => healReason; }
        public EHealType HealType { get => healType; }
        public int HealAmount { get => healAmount; }
        public float HealPercentage { get => healPercentage; }

        public Heal(int amount)
        {
            healAmount = amount;
        }

        public Heal(int amount, EHealReason damageReason)
        {
            healAmount = amount;
            this.healReason = damageReason;
        }

        public Heal(int amount, float percentage, EHealReason damageReason, EHealType damageType)
        {
            healAmount = amount;
            healPercentage = percentage;
            this.healReason = damageReason;
            this.healType = damageType;
        }
    }
}