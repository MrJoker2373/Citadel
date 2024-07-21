namespace Citadel.Unity
{
    using UnityEngine;
    public sealed class CameraController
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private Vector3 _direction;
        private bool _isMoving;
        public CameraController(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }
        public void Move()
        {
            _isMoving = true;
        }
        public void Stop()
        {
            _isMoving = false;
        }
        public void Rotate(Vector3 direction)
        {
            _direction = direction;
        }
        public void LastUpdate(in float delta)
        {
            if(_isMoving == true)
                _transform.position += _direction * _speed * delta;
        }
    }
}