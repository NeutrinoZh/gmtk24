using DG.Tweening;
using GMTK.Services;
using GMTK.UI;
using UnityEngine;

namespace GMTK.GameStates
{
    public class TransitionIntoMicroWorld
    {
        private const float k_transitionDirection = 1f;

        private Transform _player;
        private CameraFollow _camera;

        public void Start(GamePlayState _playState)
        {
            _camera = ServiceLocator.Instance.Get<CameraFollow>();
            _player = ServiceLocator.Instance.Get<PlayerStats>().Player;
            var nearestCell = ServiceLocator.Instance.Get<CellManager>().FindNearToPoint(_player.position);

            _player.GetComponent<DriftMovableObject>().Stop();
            _player.GetComponent<BoxCollider2D>().isTrigger = true;
            nearestCell.GetComponent<Rigidbody2D>().isKinematic = true;

            ServiceLocator.Instance.Get<PlayerStats>().Speed = 0.01f;

            _camera.Target = nearestCell;

            var insidePosition = _player.position + (nearestCell.position - _player.position) / 2;

            DOTween.To(
                () => _player.transform.localScale,
                scale => _player.localScale = scale,
                new Vector3(0.25f, 0.25f, 1),
                k_transitionDirection
            );

            DOTween.To(
                () => Camera.main.orthographicSize,
                size => Camera.main.orthographicSize = size,
                1.9f,
                k_transitionDirection
            );

            DOTween.To(
                () => _camera.transform.rotation.z,
                angle => _camera.transform.rotation = new Quaternion(_camera.transform.rotation.x, _camera.transform.rotation.y, angle, _camera.transform.rotation.w),
                nearestCell.rotation.z,
                k_transitionDirection
            );

            DOTween.To(
               () => _player.position,
               position => _player.position = position,
               insidePosition,
               k_transitionDirection
           ).OnComplete(() =>
           {
               _player.gameObject.layer = LayerMask.NameToLayer("InsideCell");
               _player.GetComponent<BoxCollider2D>().isTrigger = false;
               _player.GetComponent<Rigidbody2D>().isKinematic = false;

               ServiceLocator.Instance.Get<HUD>().AdviceGetOutSet(true);
           });
        }

    }
}