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
        [SerializeField] private float _range;
        private Plane _plane;
        private Vector3 _offset;

        private void LateUpdate()
        {
            if(_target != null)
            {
                var point = transform.position + _offset * _range;
                var target = _plane.ClosestPointOnPlane(_target.position);
                transform.position = Vector3.Lerp(point, target, LERP_THRESHOLD);
            }
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
            var input = context.ReadValue<Vector2>();
            var offset = Vector3.zero;
            if (_left.rect.Contains(_left.InverseTransformPoint(input)))
                offset += -transform.right;
            else if (_right.rect.Contains(_right.InverseTransformPoint(input)))
                offset += transform.right;
            if (_down.rect.Contains(_down.InverseTransformPoint(input)))
                offset += -transform.up;
            else if (_up.rect.Contains(_up.InverseTransformPoint(input)))
                offset += transform.up;
            _offset = offset.normalized;
        }
    }
}