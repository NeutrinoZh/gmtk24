using System;
using UnityEngine;

namespace GMTK 
{
    public class CellStats : Stats, IDamageable
    {
        public override event Action<int> OnHealthChanged;

        void IDamageable.Damage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }
    }
}