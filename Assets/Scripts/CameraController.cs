namespace Citadel
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [SerializeField] private float _speed;
        [SerializeField] private OrientationController _orientation;
        private Vector3 _direction;

        private void LateUpdate() => UpdatePosition();

        public void Compose() => Enable();

        public void Enable()
        {
            _mousePosition.Enable();
            _mousePosition.performed += MousePosition;
        }

        public void UpdatePosition()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }

        public void Disable()
        {
            _mousePosition.Disable();
            _mousePosition.performed -= MousePosition;
        }

        private void MousePosition(InputAction.CallbackContext context)
        {
            var position = context.ReadValue<Vector2>();
            var direction = Vector3.zero;
            if (_left.rect.Contains(_left.InverseTransformPoint(position)))
                direction += _orientation.GetLeft();
            else if (_right.rect.Contains(_right.InverseTransformPoint(position)))
                direction += _orientation.GetRight();
            if (_down.rect.Contains(_down.InverseTransformPoint(position)))
                direction += _orientation.GetDown();
            else if (_up.rect.Contains(_up.InverseTransformPoint(position)))
                direction += _orientation.GetUp();
            _direction = direction.normalized;
        }
    }
}