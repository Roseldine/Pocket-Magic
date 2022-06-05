
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class Damage
    {
        public enum EDamageReason { Unkown, Combat, FriendlyFire, Map, NPC}
        public enum EDamageType { Value, PercentMaxHealth, PercentRemainHealth }

        [SerializeField] EDamageReason damageReason;
        [SerializeField] EDamageType damageType;
        [SerializeField] int damageAmount;
        [SerializeField] float damagePercentage;

        public EDamageReason DamageReason { get => damageReason; }
        public EDamageType DamageType { get => damageType; }
        public int DamageAmount { get => damageAmount; }
        public float DamagePercentage { get => damagePercentage; }

        public Damage(int amount)
        {
            damageAmount = amount;
        }

        public Damage(int amount, EDamageReason damageReason)
        {
            damageAmount = amount;
            this.damageReason = damageReason;
        }

        public Damage(int amount, float percentage, EDamageReason damageReason, EDamageType damageType)
        {
            damageAmount = amount;
            damagePercentage = percentage;
            this.damageReason = damageReason;
            this.damageType = damageType;
        }
    }
}