using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerBodyController : MonoBehaviour
    {
        [SerializeField] private float _friction;
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;

        [SerializeField] private float _angularFriction;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _angularAcceleration;

        private InputController _input;

        private Vector3 _velocity;
        private float _angularVelocity;

        private void Awake()
        {
            _input = ServiceLocator.Instance.Get<InputController>();
        }

        private void Update()
        {
            transform.position += Time.deltaTime * _velocity;
            transform.Rotate(0, 0, Time.deltaTime * _angularVelocity);

#if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + _velocity, Color.green);
            Debug.DrawLine(transform.position, transform.position + transform.right);
#endif
        }

        private void FixedUpdate()
        {
            var inputValue = _input.Actions.PlayerBody.Move.ReadValue<Vector2>();

            _velocity += inputValue.y * _acceleration * transform.right;
            _velocity += _friction * -_velocity;
            _velocity = Vector3.ClampMagnitude(_velocity, _speed);

            _angularVelocity += inputValue.x * _angularAcceleration;
            _angularVelocity += _angularFriction * -Mathf.Sign(_angularVelocity);
            _angularVelocity = Mathf.Clamp(_angularVelocity, -_angularSpeed, _angularSpeed);
        }
    }
}