using UnityEngine;

namespace GMTK.MicroViruses
{
    public class OrangeVirus : VirusBehavior
    {
        private Transform _transform;

        public override void Init(Transform transform)
        {
            _transform = transform;

        }
    }
}