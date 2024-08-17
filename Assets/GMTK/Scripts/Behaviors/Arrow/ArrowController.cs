using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private Arrow _arrow;

        private const float k_findTargetInterval = 0.1f;

        private void Start()
        {
            StartCoroutine(StartFindTargets(k_findTargetInterval));
        }

        private void FindTarget()
        {
            var targetPosition = _arrow.FindTargetPosition();
            if (targetPosition == null) return;

            var isTargetInCamera = CheckTargetPosition(targetPosition);

            if (isTargetInCamera) _arrow.SetArrowState(false);
            else _arrow.SetArrowState(true);
        }

        private bool CheckTargetPosition(Transform target)
        {
            if (target.position.x >= Camera.main.OrthographicBounds().min.x &&
                target.position.x <= Camera.main.OrthographicBounds().max.x &&
                target.position.y >= Camera.main.OrthographicBounds().min.y &&
                target.position.y <= Camera.main.OrthographicBounds().max.y)

                return true;
            else
                return false;
        }

        private IEnumerator StartFindTargets(float waitTime)
        {
            while (true)
            {
                FindTarget();
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}

