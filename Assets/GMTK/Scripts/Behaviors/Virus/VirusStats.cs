using System;
using UnityEngine;

namespace GMTK
{
    public class VirusStats : Stats, IDamageable
    {
        [field: SerializeField] public float DelayBeforePenetration { get; private set; }

        void IDamageable.Damage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }
    }
}
