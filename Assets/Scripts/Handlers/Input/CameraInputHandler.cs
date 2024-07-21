namespace Citadel.Unity.Handlers.Input
{
    using UnityEngine;
    using Citadel.Unity.Management;
    using Citadel.Unity.Management.Input;
    public sealed class CameraInputHandler : IMousePositionHandler
    {
        private readonly RectTransform _left;
        private readonly RectTransform _right;
        private readonly RectTransform _down;
        private readonly RectTransform _up;
        private readonly OrientationController _orientation;
        private readonly CameraController _camera;
        public CameraInputHandler(
            RectTransform left,
            RectTransform right,
            RectTransform down,
            RectTransform up,
            OrientationController orientation,
            CameraController camera)
        {
            _left = left;
            _right = right;
            _down = down;
            _up = up;
            _orientation = orientation;
            _camera = camera;
        }
        public void MousePosition(in Vector2 position)
        {
            var direction = Vector3.zero;
            if (_left.rect.Contains(_left.InverseTransformPoint(position)))
                direction += -_orientation.GetXAxis();
            else if (_right.rect.Contains(_right.InverseTransformPoint(position)))
                direction += _orientation.GetXAxis();
            if (_down.rect.Contains(_down.InverseTransformPoint(position)))
                direction += -_orientation.GetZAxis();
            else if (_up.rect.Contains(_up.InverseTransformPoint(position)))
                direction += _orientation.GetZAxis();
            if (direction == Vector3.zero)
                _camera.Stop();
            else
            {
                _camera.Rotate(direction);
                _camera.Move();
            }
        }
    }
}