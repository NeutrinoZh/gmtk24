using GMTK.Services;
using GMTK.VirusBehaviors;
using System.Collections;
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

            ChangeBehavior(new MoveToCell());
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out CellController cell))
            {
                ChangeBehavior(new AttackCell(cell.transform));
                StartCoroutine(StartPenetrationAfterDelay(cell.transform));
            }
        }

        private IEnumerator StartPenetrationAfterDelay(Transform cell)
        {
            yield return new WaitForSeconds(_virusStats.DelayBeforePenetration);
            ChangeBehavior(new PenetrationIntoCell(cell));
        }


        private void ChangeBehavior(IBehavior behavior)
        {
            _behavior = behavior;
            _behavior.Init(transform);
        }
    }
}