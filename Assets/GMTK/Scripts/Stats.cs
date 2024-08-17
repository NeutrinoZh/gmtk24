using System;
using UnityEngine;

namespace GMTK
{
    public abstract class Stats : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public int Health { get; protected set; }
        public Action<int> OnHealthChanged;

        public void Init() => Health = _maxHealth;
    }
}