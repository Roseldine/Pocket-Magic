
using UnityEngine;

namespace StardropTools
{
    public interface IDamageable
    {
        public void ApplyDamage(int amount);
        public void ApplyHeal(int amount);
        public void Die();
    }
}
