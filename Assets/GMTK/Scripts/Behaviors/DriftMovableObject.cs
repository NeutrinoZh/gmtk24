using System;
using UnityEngine;

namespace GMTK
{
    public class DriftMovableObject : MonoBehaviour
    {
        public event Action<float> OnSpeedChanged;
        [field:SerializeField] public float MaxSpeed { get; private set; }
        [SerializeField] private float _friction;
        [SerializeField] private float _acceleration;

        [SerializeField] private float _angularFriction;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _angularAcceleration;

        private Vector3 _velocity;
        private float _angularVelocity;

        private void Update()
        {
            transform.position += Time.deltaTime * _velocity;
            transform.Rotate(0, 0, Time.deltaTime * _angularVelocity);

#if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + _velocity, Color.green);
            Debug.DrawLine(transform.position, transform.position + transform.right);
#endif
        }

        public void Move(float direction)
        {
            _velocity += direction * _acceleration * transform.right;
            _velocity += _friction * -_velocity;
            _velocity = Vector3.ClampMagnitude(_velocity, MaxSpeed);
            OnSpeedChanged?.Invoke(_velocity.magnitude);
        }

        public void Rotate(float direction)
        {
            _angularVelocity += direction * _angularAcceleration;
            _angularVelocity += _angularFriction * -Mathf.Sign(_angularVelocity);
            _angularVelocity = Mathf.Clamp(_angularVelocity, -_angularSpeed, _angularSpeed);
        }

        public void Stop()
        {
            _velocity = Vector3.zero;
            _angularVelocity = 0;
        }
    }
}