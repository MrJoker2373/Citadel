namespace Citadel.Unity.Camera
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Components;
    public sealed class CameraInput
    {
        private readonly InputAction _mousePosition;
        private readonly RectTransform _left;
        private readonly RectTransform _right;
        private readonly RectTransform _bottom;
        private readonly RectTransform _top;
        private readonly Transform _pivot;
        private readonly TransformMovement _movement;
        public CameraInput(
            InputAction mousePosition,
            RectTransform left,
            RectTransform right,
            RectTransform bottom,
            RectTransform top,
            Transform pivot,
            TransformMovement movement)
        {
            _mousePosition = mousePosition;
            _mousePosition.Enable();
            _mousePosition.performed += OnMousePositionPerformed;
            _left = left;
            _right = right;
            _bottom = bottom;
            _top = top;
            _pivot = pivot;
            _movement = movement;
        }
        private void OnMousePositionPerformed(InputAction.CallbackContext context)
        {
            var position = context.ReadValue<Vector2>();
            var isLeft = _left.rect.Contains(_left.InverseTransformPoint(position));
            var isRight = _right.rect.Contains(_right.InverseTransformPoint(position));
            var isBottom = _bottom.rect.Contains(_bottom.InverseTransformPoint(position));
            var isTop = _top.rect.Contains(_top.InverseTransformPoint(position));
            if (isLeft == false && isRight == false && isBottom == false && isTop == false)
                _movement.Stop();
            else
            {
                var direction = Vector3.zero;
                if (isLeft == true)
                    direction += -_pivot.right;
                else if (isRight == true)
                    direction += _pivot.right;
                if (isBottom == true)
                    direction += -_pivot.up;
                else if (isTop == true)
                    direction += _pivot.up;
                _movement.SetDirection(direction);
                _movement.Move();
            }
        }
    }
}