using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class VirusController : MonoBehaviour
    {
        private CellManager _cellManager;
        private VirusStats _virusStats;
        private DriftMovableObject _body;
        private Transform _target;

        private void Start()
        {
            _body = GetComponent<DriftMovableObject>();
            _virusStats = GetComponent<VirusStats>();
            _virusStats.Init();
            _virusStats.OnHealthChanged += HandleHealthChanged;
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _target = _cellManager.FindNearToPoint(transform.position);
        }

        private void FixedUpdate()
        {
            var direction = _target.position - transform.position;
            float product = transform.right.x * direction.y - transform.right.y * direction.x;

            _body.Rotate(Mathf.Sign(product));
            _body.Move(1f);
        }

        private void HandleHealthChanged(int currentHealth) 
        {
            if (currentHealth <= 0) 
            {
                Destroy(gameObject);
            }
        }
    }
}