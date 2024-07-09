namespace Citadel.Unity.Components
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine;
    public sealed class TransformMovement
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private Vector3 _direction;
        private bool _isStopped;
        public TransformMovement(Transform transform, float speed)
        {
            if (speed < 0)
                throw new ArgumentException("Speed is less than 0");
            _transform = transform;
            _speed = speed;
        }
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
        public async Task Move()
        {
            if (_isStopped == false)
                return;
            _isStopped = false;
            while (_isStopped == false)
            {
                _transform.position += _speed * Time.deltaTime * _direction;
                await Task.Delay((int)(Time.deltaTime * 1000));
            }
        }
        public void Stop()
        {
            _isStopped = true;
        }
    }
}