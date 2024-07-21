namespace Citadel.Unity.Management.Input
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public sealed class InputController
    {
        private readonly InputAction _mousePosition;
        private readonly InputAction _movementKey;
        private readonly InputAction _rollKey;
        private readonly List<IMousePositionHandler> _mousePositionHandlers;
        private readonly List<IMovementKeyHandler> _movementKeyHandlers;
        private readonly List<IRollKeyHandler> _rollKeyHandlers;
        public InputController(InputAction mousePosition, InputAction movementKey, InputAction rollKey)
        {
            _mousePosition = mousePosition;
            _movementKey = movementKey;
            _rollKey = rollKey;
            _mousePositionHandlers = new();
            _movementKeyHandlers = new();
            _rollKeyHandlers = new();
            Enable();
        }
        public void AddMousePositionHandler(IMousePositionHandler handler)
        {
            _mousePositionHandlers.Add(handler);
        }
        public void AddMovementKeyHandler(IMovementKeyHandler handler)
        {
            _movementKeyHandlers.Add(handler);
        }
        public void AddRollKeyHandler(IRollKeyHandler handler)
        {
            _rollKeyHandlers.Add(handler);
        }
        public void RemoveMousePositionHandler(IMousePositionHandler handler)
        {
            _mousePositionHandlers.Remove(handler);
        }
        public void RemoveMovementKeyHandler(IMovementKeyHandler handler)
        {
            _movementKeyHandlers.Remove(handler);
        }
        public void RemoveRollKeyHandler(IRollKeyHandler handler)
        {
            _rollKeyHandlers.Remove(handler);
        }
        private void Enable()
        {
            _mousePosition.Enable();
            _movementKey.Enable();
            _rollKey.Enable();
            _mousePosition.performed += MousePosition;
            _movementKey.performed += MovementKey;
            _rollKey.performed += RollKey;
        }
        private void Disable()
        {
            _mousePosition.Disable();
            _movementKey.Disable();
            _rollKey.Disable();
            _mousePosition.performed -= MousePosition;
            _movementKey.performed -= MovementKey;
            _rollKey.performed -= RollKey;
        }
        private void MousePosition(InputAction.CallbackContext context)
        {
            var position = context.ReadValue<Vector2>();
            _mousePositionHandlers.ForEach(handler => handler.MousePosition(position));
        }
        private void MovementKey(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _movementKeyHandlers.ForEach(handler => handler.MovementKey(direction));
        }
        private void RollKey(InputAction.CallbackContext context)
        {
            _rollKeyHandlers.ForEach(hanler => hanler.RollKey());
        }
    }
}