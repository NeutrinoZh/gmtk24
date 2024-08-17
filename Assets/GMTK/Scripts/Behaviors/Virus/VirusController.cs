using GMTK.Services;
using GMTK.VirusBehaviors;
using UnityEngine;

namespace GMTK
{
    public class VirusController : MonoBehaviour
    {
        private CellManager _cellManager;
        private VirusStats _virusStats;
        private DriftMovableObject _body;
        private Transform _target;

        private IBehavior _behavior;

        private void Start()
        {
            _body = GetComponent<DriftMovableObject>();
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _target = _cellManager.FindNearToPoint(transform.position);

            _virusStats = GetComponent<VirusStats>();
            _virusStats.Init();
            _virusStats.OnHealthChanged += HandleHealthChanged;

            _behavior = new MoveToCell();
            _behavior.Init(transform);
        }

        private void FixedUpdate()
        {
            _behavior.FixedUpdate();
        }

        private void HandleHealthChanged(int currentHealth)
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out CellController cell))
            {
                _behavior = new AttackCell(cell.transform);
                _behavior.Init(transform);
            }
        }
    }
}