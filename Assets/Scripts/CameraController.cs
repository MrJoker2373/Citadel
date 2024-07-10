namespace Citadel.Unity
{
    using UnityEngine;
    using Citadel.Unity.Input;
    using Citadel.Unity.Movement;
    public sealed class CameraController : IMousePositionObserver
    {
        private readonly OrientationController _orientation;
        private readonly MovementController _movement;
        private readonly RectTransform _left;
        private readonly RectTransform _right;
        private readonly RectTransform _down;
        private readonly RectTransform _up;
        public CameraController(
            OrientationController orientation,
            MovementController movement,
            RectTransform left,
            RectTransform right,
            RectTransform down,
            RectTransform up)
        {
            _orientation = orientation;
            _movement = movement;
            _left = left;
            _right = right;
            _down = down;
            _up = up;
        }
        public void OnMousePositionChanged(Vector2 input)
        {
            var isLeft = _left.rect.Contains(_left.InverseTransformPoint(input));
            var isRight = _right.rect.Contains(_right.InverseTransformPoint(input));
            var isDown = _down.rect.Contains(_down.InverseTransformPoint(input));
            var isUp = _up.rect.Contains(_up.InverseTransformPoint(input));
            if (isLeft == false && isRight == false && isDown == false && isUp == false)
                _movement.Stop();
            else
            {
                var direction = _orientation.GetDirection(isLeft, isRight, isDown, isUp);
                _movement.SetDirection(direction.normalized);
                if (_movement.IsMoving() == false)
                    _movement.Move();
            }
        }
    }
}