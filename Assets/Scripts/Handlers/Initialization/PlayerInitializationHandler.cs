namespace Citadel.Unity.Handlers.Initialization
{
    using UnityEngine;
    using Citadel.Unity.Management;
    using Citadel.Unity.Management.Initialization;
    using Citadel.Unity.Management.Input;
    using Citadel.Unity.Handlers.Input;
    public sealed class PlayerInitializationHandler : 
        MonoBehaviour, 
        IInitializationHandler,
        IPhysicsUpdateHandler
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationController _animation;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rollSpeed;
        private OrientationController _orientation;
        private PlayerController _controller;
        private PlayerInputHandler _inputHandler;
        private InputController _inputController;
        public void Install(in InitializationData data)
        {
            _orientation = data.Get<OrientationController>();
            _controller = new PlayerController(_rigidbody, _animation, _movementSpeed, _rollSpeed);
            _inputHandler = new PlayerInputHandler(_controller, _orientation);
            _inputController = data.Get<InputController>();
            _animation.Install();
            _animation.Add(_controller);
            _inputController.AddMovementKeyHandler(_inputHandler);
            _inputController.AddRollKeyHandler(_inputHandler);
        }
        public void PhysicsUpdate(in float delta)
        {
            _controller.PhysicsUpdate(delta);
        }
    }
}