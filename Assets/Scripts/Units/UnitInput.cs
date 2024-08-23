namespace Citadel.Units
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class UnitInput
    {
        private InputAction _movementInput;
        private InputAction _rollInput;
        private InputAction _attackInput;
        private InputAction _collectInput;
        private IOrientationController _orientation;
        private UnitMachine _machine;
        private UnitRotation _rotation;
        private UnitInventory _inventory;

        public void Compose(
            InputAction movementInput,
            InputAction rollInput,
            InputAction attackInput,
            InputAction collectInput,
            IOrientationController orientation,
            UnitMachine machine,
            UnitRotation rotation,
            UnitInventory inventory)
        {
            _movementInput = movementInput;
            _rollInput = rollInput;
            _attackInput = attackInput;
            _collectInput = collectInput;
            _orientation = orientation;
            _machine = machine;
            _rotation = rotation;
            _inventory = inventory;
        }

        public void Enable()
        {
            _movementInput.Enable();
            _rollInput.Enable();
            _attackInput.Enable();
            _collectInput.Enable();
            _movementInput.performed += Move;
            _rollInput.performed += Roll;
            _attackInput.performed += Attack;
            _collectInput.performed += Collect;
        }

        public void Disable()
        {
            _movementInput.Disable();
            _rollInput.Disable();
            _attackInput.Disable();
            _collectInput.Disable();
            _movementInput.performed -= Move;
            _rollInput.performed -= Roll;
            _attackInput.performed -= Attack;
            _collectInput.performed -= Collect;
        }

        private void Move(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            var direction = input.x * _orientation.GetRight() + input.y * _orientation.GetForward();
            _rotation.Rotate(direction);
            if (direction == Vector3.zero)
                _machine.DefaultState<UnitIdle>();
            else
                _machine.DefaultState<UnitMovement>();
        }

        private void Roll(InputAction.CallbackContext context)
        {
            _machine.SpecialState<UnitRoll>();
        }

        private void Attack(InputAction.CallbackContext context)
        {
            _machine.SpecialState<UnitAttack>();
        }

        private void Collect(InputAction.CallbackContext context)
        {
            _inventory.CollectItem();
        }
    }
}