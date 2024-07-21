namespace Citadel.Unity
{
    using UnityEngine;
    using Citadel.Unity.Management;
    public sealed class PlayerController : IEventUpdateHandler
    {
        private const string IDLE_KEY = "Idle";
        private const string MOVE_KEY = "Move";
        private const string ROLL_KEY = "Roll";
        private const string ROLL_STOP = "RollStop";
        private readonly Rigidbody _rigidbody;
        private readonly AnimationController _animation;
        private readonly float _movementSpeed;
        private readonly float _rollSpeed;
        private Vector3 _direction;
        private Vector3 _rawDirection;
        private float _speed;
        private bool _isMoving;
        private bool _isMovingState;
        private bool _isRollingState;
        public PlayerController(
            Rigidbody rigidbody,
            AnimationController animation,
            float movementSpeed,
            float rollSpeed)
        {
            _rigidbody = rigidbody;
            _animation = animation;
            _movementSpeed = movementSpeed;
            _rollSpeed = rollSpeed;
        }
        public void Rotate(Vector3 direction)
        {
            _rawDirection = direction;
            if (_isRollingState == false)
                _direction = direction;
        }
        public void Idle()
        {
            _isMoving = false;
            if (_isRollingState == true)
                return;
            _isMovingState = false;
            _animation.Play(IDLE_KEY);
        }
        public void Move()
        {
            _isMoving = true;
            if (_isRollingState == true)
                return;
            _animation.Play(MOVE_KEY);
            _speed = _movementSpeed;
            _isMovingState = true;
        }
        public void Roll()
        {
            _isRollingState = true;
            _speed = _rollSpeed;
            _isMovingState = true;
            _animation.Play(ROLL_KEY);
        }
        public void PhysicsUpdate(in float delta)
        {
            if (_isMovingState == true)
                _rigidbody.velocity += _direction * _speed * delta;
            var targetRotation = Quaternion.LookRotation(_direction);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, 0.4f);
        }
        public void EventUpdate(in string name)
        {
            if (name == ROLL_STOP)
            {
                _isRollingState = false;
                _direction = _rawDirection;
                if (_isMoving == false)
                    Idle();
                else
                    Move();
            }
        }
    }
}