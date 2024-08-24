namespace Citadel.Units
{
    using UnityEngine;

    public class UnitPhysics
    {
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _speed;
        private bool _isActive;

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void Compose(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Update()
        {
            if (_isActive == true)
                _rigidbody.linearVelocity += _speed * _direction;
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}