namespace Citadel.Unity.Handlers.Initialization
{
    using UnityEngine;
    using Citadel.Unity.Management;
    using Citadel.Unity.Management.Initialization;
    using Citadel.Unity.Management.Input;
    using Citadel.Unity.Handlers.Input;
    public sealed class CameraInitializationHandler :
        MonoBehaviour, 
        IInitializationHandler, 
        ILastUpdateHandler
    {
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;
        private OrientationController _orientation;
        private CameraController _controller;
        private CameraInputHandler _inputHandler;
        private InputController _inputController;
        public void Install(in InitializationData data)
        {
            _orientation = data.Get<OrientationController>();
            _controller = new CameraController(_transform, _speed);
            _inputHandler = new CameraInputHandler(_left, _right, _down, _up, _orientation, _controller);
            _inputController = data.Get<InputController>();
            _inputController.AddMousePositionHandler(_inputHandler);
        }
        public void LastUpdate(in float delta)
        {
            _controller.LastUpdate(delta);
        }
    }
}