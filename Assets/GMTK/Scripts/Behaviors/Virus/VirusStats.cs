using System;

namespace GMTK
{
    public class VirusStats : Stats, IDamageable
    {
        public override event Action<int> OnHealthChanged;

        void IDamageable.Damage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }
    }
}
