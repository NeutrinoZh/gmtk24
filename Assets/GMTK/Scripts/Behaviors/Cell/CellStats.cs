using System;
using UnityEngine;

namespace GMTK
{
    public class CellStats : Stats, IDamageable
    {
        void IDamageable.Damage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }
    }
}