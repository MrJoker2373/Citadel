namespace Citadel.Unity.Entities
{
    using UnityEngine;
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Vector3 _direction;
        private bool _isMoving;
        private void LateUpdate()
        {
            if (_isMoving == true)
                transform.position += _direction * _speed * Time.deltaTime;
        }
        public void Move(Vector3 direction)
        {
            _isMoving = true;
            _direction = direction.normalized;
        }
        public void Stop()
        {
            _isMoving = false;
        }
    }
}