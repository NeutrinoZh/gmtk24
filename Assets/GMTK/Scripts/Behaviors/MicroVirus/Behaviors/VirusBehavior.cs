using UnityEngine;

namespace GMTK.MicroViruses
{
    public abstract class VirusBehavior
    {

        public virtual void Init(Transform transform) { }
        public virtual void Update() { }
        public virtual void OnEnable() { }
        public virtual void FixedUpdate() { }
        public virtual void OnDestroy() { }
        public virtual void OnDamage() { }

    }
}