namespace Citadel.Unity.Units.Components
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Core;
    public class UnitInput
    {
        private readonly InputAction _movementInput;
        private readonly InputAction _rollInput;
        private readonly InputAction _attackInput;
        private readonly InputAction _collectInput;
        private readonly OrientationController _orientation;
        private readonly UnitMovement _movement;
        private readonly UnitRoll _roll;
        private readonly UnitAttack _attack;
        private readonly UnitInventory _inventory;
        private ICollectible _collectible;
        public UnitInput(
            InputAction movementInput,
            InputAction rollInput,
            InputAction attackInput,
            InputAction collectInput,
            OrientationController orientation,
            UnitMovement movement,
            UnitRoll roll,
            UnitAttack attack,
            UnitInventory inventory)
        {
            _movementInput = movementInput;
            _rollInput = rollInput;
            _attackInput = attackInput;
            _collectInput = collectInput;
            _orientation = orientation;
            _movement = movement;
            _roll = roll;
            _attack = attack;
            _inventory = inventory;
            Enable();
        }
        private void Move(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            if (input == Vector2.zero)
                _movement.IdleState();
            else
            {
                var direction = input.x * _orientation.GetRight() + input.y * _orientation.GetForward();
                _movement.SetDirection(direction);
                _movement.MoveState();
            }
        }
        private async void Roll(InputAction.CallbackContext context)
        {
            if (_movement.IsEnabled() == true)
            {
                _movement.ExitState();
                await _roll.ProcessState();
                _movement.EnterState();
            }
        }
        private async void Attack(InputAction.CallbackContext context)
        {
            if (_movement.IsEnabled() == true)
            {
                _movement.ExitState();
                await _attack.ProcessState();
                _movement.EnterState();
            }
        }
        private void Collect(InputAction.CallbackContext context)
        {
            if (_collectible == null)
                return;
            if (_collectible.IsType(ICollectible.CollectibleType.Bomb))
                _inventory.AddBomb();
            _collectible.Collect();
            _collectible = null;
        }
        private void Enable()
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
        private void Disable()
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
        public void SetCollectible(ICollectible collectible)
        {
            if (collectible.IsPassive() == false)
                _collectible = collectible;
            else
            {
                if (collectible.IsType(ICollectible.CollectibleType.Coin))
                    _inventory.AddCoin();
                collectible.Collect();
            }
        }
    }
}