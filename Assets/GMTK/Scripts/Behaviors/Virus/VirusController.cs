using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class VirusController : MonoBehaviour, IDamageable
    {
        private CellManager _cellManager;
        private VirusManager _virusManager;

        private DriftMovableObject _body;
        private Transform _target;

        private void Start()
        {
            _body = GetComponent<DriftMovableObject>();
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _target = _cellManager.FindNearToPoint(transform.position);

            _virusManager = ServiceLocator.Instance.Get<VirusManager>();
        }

        private void FixedUpdate()
        {
            var direction = _target.position - transform.position;
            float product = transform.right.x * direction.y - transform.right.y * direction.x;

            _body.Rotate(Mathf.Sign(product));
            _body.Move(1f);
        }

        void IDamageable.Damage(int damage)
        {
            _virusManager.RemoveObject(transform);
            Destroy(gameObject);
        }
    }
}