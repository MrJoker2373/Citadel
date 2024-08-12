namespace Citadel.Unity.Entities.Camera
{
    using UnityEngine;
    public class CameraMovement
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private Vector3 _direction;
        public CameraMovement(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }
        public void Update()
        {
            _transform.position += _speed * Time.deltaTime * _direction;
        }
        public void Move(Vector3 direction)
        {
            _direction = direction.normalized;
        }
    }
}