using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerBodyController : MonoBehaviour
    {
        private InputController _input;
        private DriftMovableObject _body;

        private void Awake()
        {
            _input = ServiceLocator.Instance.Get<InputController>();
            _body = transform.GetComponent<DriftMovableObject>();
        }

        private void FixedUpdate()
        {
            var inputValue = _input.Actions.PlayerBody.Move.ReadValue<Vector2>();

            _body.Move(inputValue.y);
            _body.Rotate(inputValue.x);
        }
    }
}