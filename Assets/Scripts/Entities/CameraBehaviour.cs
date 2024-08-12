namespace Citadel.Unity.Entities
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Core;
    using Citadel.Unity.Entities.Camera;
    public class CameraBehaviour : MonoBehaviour
    {
        [Header(nameof(CameraMovement))]
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;
        [Header(nameof(CameraInput))]
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [SerializeField] private OrientationController _orientation;
        private CameraMovement _movement;
        private CameraInput _input;
        private void Awake()
        {
            _movement = new(_transform, _speed);
            _input = new(_mousePosition, _left, _right, _down, _up, _orientation, _movement);
        }
        private void LateUpdate()
        {
            _movement.Update();
        }
    }
}