namespace Citadel.Unity.Handlers.Initialization
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Management.Initialization;
    using Citadel.Unity.Management.Input;
    public sealed class InputInitializationHandler : MonoBehaviour, IInitializationHandler
    {
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private InputAction _movementKey;
        [SerializeField] private InputAction _rollKey;
        private InputController _controller;
        public void Install(in InitializationData data)
        {
            _controller = new InputController(_mousePosition, _movementKey, _rollKey);
            data.Add(_controller);
        }
    }
}