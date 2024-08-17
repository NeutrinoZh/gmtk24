using GMTK.VirusBehaviors;
using UnityEngine;

namespace GMTK
{
    public class VirusController : MonoBehaviour, IDamageable
    {
        private IBehavior _behavior;

        private void Start()
        {
            _behavior = new MoveToCell();
            _behavior.Init(transform);
        }

        private void FixedUpdate()
        {
            _behavior.FixedUpdate();
        }

        void IDamageable.Damage(int damage)
        {
            Destroy(gameObject);
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