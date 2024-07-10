namespace Citadel.Unity.Input
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public sealed class InputController
    {
        private readonly InputAction _mousePosition;
        private readonly InputAction _movementDirection;
        private readonly List<IMousePositionObserver> _mousePositionObservers;
        private readonly List<IMovementDirectionObserver> _movementDirectionObservers;
        public InputController(
            InputAction mousePosition, 
            InputAction movementDirection)
        {
            _mousePosition = mousePosition;
            _movementDirection = movementDirection;
            _mousePositionObservers = new();
            _movementDirectionObservers = new();
            _mousePosition.performed += OnMousePositionPerformed;
            _movementDirection.performed += OnMovementDirectionPerformed;
        }
        public void Enable()
        {
            _mousePosition.Enable();
            _movementDirection.Enable();
        }
        public void Disable()
        {
            _mousePosition.Disable();
            _movementDirection.Disable();
        }
        public void AddMousePositionObserver(IMousePositionObserver observer)
        {
            _mousePositionObservers.Add(observer);
        }
        public void AddMovementDirectionObserver(IMovementDirectionObserver observer)
        {
            _movementDirectionObservers.Add(observer);
        }
        public void RemoveMousePositionObserver(IMousePositionObserver observer)
        {
            _mousePositionObservers.Remove(observer);
        }
        public void RemoveMovementDirectionObserver(IMovementDirectionObserver observer)
        {
            _movementDirectionObservers.Remove(observer);
        }
        private void OnMousePositionPerformed(InputAction.CallbackContext context)
        {
            var position = context.ReadValue<Vector2>();
            _mousePositionObservers.ForEach(observer => observer.OnMousePositionChanged(position));
        }
        private void OnMovementDirectionPerformed(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _movementDirectionObservers.ForEach(observer => observer.OnMovementDirectionChanged(direction));
        }
    }
}