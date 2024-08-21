using GMTK.GameStates;
using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private Arrow _arrow;

        private GamePlayState _gamePlayState;
        private const float k_findTargetInterval = 0.1f;

        private void Start()
        {
            _gamePlayState = ServiceLocator.Instance.Get<GamePlayState>();

            StartCoroutine(StartFindTargets(k_findTargetInterval));
        }

        private void FindTarget()
        {
            var targetPosition = _arrow.FindTargetPosition();
            if (targetPosition == null || _gamePlayState.State == WorldState.MICRO_WORLD)
            {
                _arrow.SetArrowState(false);
                return;
            }

            var isTargetInCamera = CheckTargetPosition(targetPosition);

            if (isTargetInCamera) _arrow.SetArrowState(false);
            else _arrow.SetArrowState(true);
        }

        private bool CheckTargetPosition(Transform target)
        {
            if (target.position.x >= Camera.main.OrthographicBounds().min.x / 2 &&
                target.position.x <= Camera.main.OrthographicBounds().max.x / 2 &&
                target.position.y >= Camera.main.OrthographicBounds().min.y / 2 &&
                target.position.y <= Camera.main.OrthographicBounds().max.y / 2)

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

