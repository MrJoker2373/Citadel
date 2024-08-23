namespace Citadel
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using TMPro;
    using Citadel.Units;

    public class PlayerController : UnitController
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody[] _ragdollRigidbodies;
        [SerializeField] private Collider[] _ragdollColliders;
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _bombsLabel;
        [SerializeField] private InputAction _movementInput;
        [SerializeField] private InputAction _rollInput;
        [SerializeField] private InputAction _attackInput;
        [SerializeField] private InputAction _collectInput;
        [SerializeField] private OrientationController _orientation;

        [SerializeField] private float _speedAmount;
        [SerializeField] private int _damageAmount;
        [SerializeField] private int _healthAmount;
        [SerializeField] private int _deathDelay;

        private UnitAnimation _animation;
        private UnitRagdoll _ragdoll;
        private UnitRotation _rotation;

        private UnitMachine _machine;
        private UnitIdle _idle;
        private UnitMovement _movement;
        private UnitAttack _attack;
        private UnitHealth _health;
        private UnitDeath _death;
        private UnitRoll _roll;
        private UnitInventory _inventory;
        private UnitInput _input;
        private List<object> _container;

        private void FixedUpdate()
        {
            _rotation.Update();
            _movement.Update();
        }

        private void LateUpdate()
        {
            _animation.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ItemController>(out var item))
                _inventory.SetItem(item);
        }

        public override void Compose()
        {
            CreateObjects();
            ComposeObjects();
            ConfigurateObjects();
        }

        public override void Dispose()
        {
            Destroy(gameObject);
        }

        public override T GetUnitComponent<T>()
        {
            return _container.OfType<T>().Single();
        }

        private void CreateObjects()
        {
            _animation = new();
            _ragdoll = new();
            _rotation = new();
            _machine = new();
            _idle = new();
            _movement = new();
            _attack = new();
            _health = new();
            _death = new();
            _roll = new();
            _inventory = new();
            _input = new();
            _container = new List<object>()
            {
                _animation,
                _ragdoll,
                _rotation,
                _machine,
                _idle,
                _movement,
                _attack,
                _health,
                _death,
                _roll,
                _input,
            };
        }

        private void ComposeObjects()
        {
            _animation.Compose(_animator);
            _ragdoll.Compose(_animation, _rigidbody, _collider, _ragdollRigidbodies, _ragdollColliders);
            _rotation.Compose(_machine, _movement, _rigidbody);
            var defaultStates = new IDefaultState[] { _idle, _movement };
            var specialStatess = new ISpecialState[] { _attack, _roll };
            _machine.Compose(defaultStates, specialStatess, _death);
            _idle.Compose(_animation);
            _movement.Compose(_animation, _rigidbody, _speedAmount);
            _attack.Compose(_animation, _health, _damageAmount);
            _health.Compose(_machine, _healthAmount);
            _death.Compose(_ragdoll, this, _deathDelay);
            _roll.Compose(_animation);
            _inventory.Compose(_coinsLabel, _bombsLabel);
            _input.Compose(_movementInput, _rollInput, _attackInput, _collectInput, _orientation, _machine, _rotation, _inventory);
        }

        private void ConfigurateObjects()
        {
            _rotation.Rotate(transform.forward);
            _ragdoll.Disable();
            _machine.StartState();
            _input.Enable();
        }
    }
}