using UnityEngine;

namespace GMTK
{
    public interface IDamageable
    {
        public void Damage(int damage, Vector3 attackDirection);
    }
}