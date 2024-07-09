namespace Citadel.Unity.Camera
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Components;
    public sealed class CameraInstaller : MonoBehaviour
    {
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _bottom;
        [SerializeField] private RectTransform _top;
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _speed;
        private CameraInput _input;
        private TransformMovement _movement;
        private void Awake()
        {
            _movement = new(_pivot, _speed);
            _input = new(_mousePosition, _left, _right, _bottom, _top, _pivot, _movement);
        }
    }
}