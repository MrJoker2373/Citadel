namespace Citadel.Unity.Units.Components
{
    using UnityEngine;
    using Citadel.Unity.Core;
    public class UnitMovement
    {
        private readonly AnimationController _animation;
        private readonly Rigidbody _rigidbody;
        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;
        private Vector3 _direction;
        private bool _isEnabled;
        private bool _isMoving;
        public UnitMovement(
            AnimationController animation,
            Rigidbody rigidbody,
            float movementSpeed,
            float rotationSpeed)
        {
            _animation = animation;
            _rigidbody = rigidbody;
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }
        public bool IsEnabled()
        {
            return _isEnabled;
        }
        public Vector3 GetPosition()
        {
            return _rigidbody.position;
        }
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
        public void UpdateRotation()
        {
            if (_isEnabled == true && _isMoving == true)
            {
                var rotation = Quaternion.LookRotation(_direction);
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, rotation, _rotationSpeed);
            }
        }
        public void UpdateMovement()
        {
            if (_isEnabled == true && _isMoving == true)
                _rigidbody.linearVelocity += _movementSpeed * _direction;
        }
        public void EnterState()
        {
            _isEnabled = true;
            if (_isMoving == false)
                IdleState();
            else
                MoveState();
        }
        public void ExitState()
        {
            _isEnabled = false;
        }
        public void IdleState()
        {
            _isMoving = false;
            if (_isEnabled == true)
                IdleAnimation();
        }
        public void MoveState()
        {
            _isMoving = true;
            if (_isEnabled == true)
                MoveAnimation();
        }
        private void IdleAnimation()
        {
            const string key = "Idle";
            _animation.Play(key);
        }
        private void MoveAnimation()
        {
            const string key = "Move";
            _animation.Play(key);
        }
    }
}