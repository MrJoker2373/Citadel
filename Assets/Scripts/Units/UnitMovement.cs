namespace Citadel.Units
{
    using UnityEngine;

    public class UnitMovement : IDefaultState
    {
        private const string MOVE_ANIMATION = "Move";
        private UnitAnimation _animation;
        private Rigidbody _rigidbody;
        private float _speed;
        private Vector3 _direction;
        private bool _isActive;

        public bool IsActive()
        {
            return _isActive;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public void Compose(UnitAnimation animation, Rigidbody rigidbody, float speed)
        {
            _animation = animation;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public async void Start()
        {
            _isActive = true;
            await _animation.Play(MOVE_ANIMATION);
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