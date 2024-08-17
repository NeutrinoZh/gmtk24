using UnityEngine;

namespace GMTK.VirusBehaviors
{
    public interface IBehavior
    {
        public void Init(Transform transform);
        public void FixedUpdate();
    }
}