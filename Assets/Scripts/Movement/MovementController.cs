namespace Citadel.Unity.Movement
{
    using System.Threading.Tasks;
    using UnityEngine;
    public sealed class MovementController
    {
        private readonly IMovementType _type;
        private readonly float _speed;
        private Vector3 _direction;
        private bool _isMoving;
        public MovementController(IMovementType type, float speed)
        {
            _type = type;
            _speed = speed;
            _direction = Vector3.right;
        }
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
        public bool IsMoving()
        {
            return _isMoving;
        }
        public async void Move()
        {
            _isMoving = true;
            while (_isMoving == true)
            {
                _type.Move(_speed * Time.deltaTime * _direction);
                await Task.Delay((int)(Time.deltaTime * 1000));
            }
        }
        public void Stop()
        {
            _isMoving = false;
        }
    }
}