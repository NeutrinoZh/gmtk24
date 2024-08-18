using System;
using UnityEngine;

namespace GMTK
{
    public class CellStats : Stats, IDamageable
    {
        void IDamageable.Damage(int damage, Vector3 attackDirection)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }
    }
}