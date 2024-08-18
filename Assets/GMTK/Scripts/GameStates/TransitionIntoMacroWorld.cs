using DG.Tweening;
using GMTK.Services;
using GMTK.UI;
using UnityEngine;

namespace GMTK.GameStates
{
    public class TransitionIntoMacroWorld
    {
        private const float k_transitionDirection = 1f;

        private Transform _player;
        private CameraFollow _camera;

        public void Start(GamePlayState _playState)
        {
            _camera = ServiceLocator.Instance.Get<CameraFollow>();
            _player = ServiceLocator.Instance.Get<PlayerStats>().Player;

            if (_player == null)
                return;

            var nearestCell = ServiceLocator.Instance.Get<CellManager>().FindNearToPoint(_player.position);
            var cellController = nearestCell.GetComponent<CellController>();
            cellController.SetMacroSprite();
            cellController.SetVelocity(WorldState.MACRO_WORLD);

            _player.GetComponent<DriftMovableObject>().Stop();
            nearestCell.GetComponent<Rigidbody2D>().isKinematic = false;
            _player.GetChild(0).GetChild(0).localScale = new(1f, 1f, 1);

            ServiceLocator.Instance.Get<PlayerStats>().SpeedScale = 1f;

            _camera.Target = _player;

            var outsidePosition = _player.position + _player.right * 5;

            DOTween.To(
                () => _player.transform.localScale,
                scale => _player.localScale = scale,
                new Vector3(1, 1, 1),
                k_transitionDirection
            );

            DOTween.To(
                () => Camera.main.orthographicSize,
                size => Camera.main.orthographicSize = size,
                5f,
                k_transitionDirection
            );

            DOTween.To(
                () => _camera.transform.rotation.z,
                angle => _camera.transform.rotation = new Quaternion(_camera.transform.rotation.x, _camera.transform.rotation.y, angle, _camera.transform.rotation.w),
                0,
                k_transitionDirection
            );

            DOTween.To(
               () => _player.position,
               position => _player.position = position,
               outsidePosition,
               k_transitionDirection
           ).OnComplete(() =>
           {
               _player.gameObject.layer = LayerMask.NameToLayer("Default");
               _player.GetComponent<BoxCollider2D>().isTrigger = false;
               //    _player.GetComponent<Rigidbody2D>().isKinematic = false;

               ServiceLocator.Instance.Get<HUD>().AdviceGetOutSet(false);
           });
        }

    }
}