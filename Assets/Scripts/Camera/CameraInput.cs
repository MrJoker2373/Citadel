namespace Citadel.Unity.Camera
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Core;
    public class CameraInput
    {
        private readonly InputAction _mousePosition;
        private readonly RectTransform _left;
        private readonly RectTransform _right;
        private readonly RectTransform _down;
        private readonly RectTransform _up;
        private readonly OrientationController _orientation;
        private readonly CameraMovement _movement;
        public CameraInput(
            InputAction mousePosition,
            RectTransform left,
            RectTransform right,
            RectTransform down,
            RectTransform up,
            OrientationController orientation,
            CameraMovement movement)
        {
            _mousePosition = mousePosition;
            _left = left;
            _right = right;
            _down = down;
            _up = up;
            _orientation = orientation;
            _movement = movement;
            Enable();
        }
        private void Enable()
        {
            _mousePosition.Enable();
            _mousePosition.performed += MousePosition;
        }
        private void Disable()
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
            _movement.Move(direction);
        }
    }
}