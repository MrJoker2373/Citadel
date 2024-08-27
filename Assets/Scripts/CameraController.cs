namespace Citadel
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class CameraController : MonoBehaviour
    {
        private const float LERP_THRESHOLD = 0.5f;
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetAmount;
        [SerializeField] private OrientationController _orientation;
        private Plane _plane;
        private Vector3 _offset;

        private void LateUpdate()
        {
            var point = transform.position + _offset * _offsetAmount;
            var target = _plane.ClosestPointOnPlane(_target.position);
            transform.position = Vector3.Lerp(point, target, LERP_THRESHOLD);
        }

        public void Compose()
        {
            _plane = new Plane(transform.forward, transform.position);
            Enable();
        }

        public void Enable()
        {
            _mousePosition.Enable();
            _mousePosition.performed += MousePosition;
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
            _offset = direction.normalized;
        }
    }
}