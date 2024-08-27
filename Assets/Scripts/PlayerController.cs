namespace Citadel
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using TMPro;
    using Citadel.Units;

    public class PlayerController : UnitController
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _mainRigidbody;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Rigidbody _rootRigidbody;
        [SerializeField] private Collider _rootCollider;
        [SerializeField] private Rigidbody[] _ragdollRigidbodies;
        [SerializeField] private Collider[] _ragdollColliders;
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _bombsLabel;
        [SerializeField] private InputAction _rotationInput;
        [SerializeField] private InputAction _crouchInput;
        [SerializeField] private InputAction _rollInput;
        [SerializeField] private InputAction _attackInput;
        [SerializeField] private InputAction _collectInput;
        [SerializeField] private UnitContainer _container;

        [SerializeField] private float _defaultSpeedAmount;
        [SerializeField] private float _crouchSpeedAmount;
        [SerializeField] private int _damageAmount;
        [SerializeField] private int _healthAmount;
        [SerializeField] private int _deathDelay;

        private UnitAnimation _animation;
        private UnitPhysics _physics;
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

        private void FixedUpdate()
        {
            _physics.Update();
            _rotation.Update();
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

        private void CreateObjects()
        {
            _animation = new();
            _physics = new();
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
        }

        private void ComposeObjects()
        {
            _animation.Compose(_animator);
            _physics.Compose(_mainRigidbody);
            _ragdoll.Compose(_animation, _mainRigidbody, _mainCollider, _rootRigidbody, _rootCollider, _ragdollRigidbodies, _ragdollColliders);
            _rotation.Compose(_machine, _physics, _mainRigidbody);
            _machine.Compose(_idle, _movement, _attack, _roll, _death);
            _idle.Compose(_animation);
            _movement.Compose(_physics, _animation, _defaultSpeedAmount, _crouchSpeedAmount);
            _attack.Compose(_animation, _physics, _health, _damageAmount);
            _health.Compose(_machine, _healthAmount);
            _death.Compose(_ragdoll, this, _deathDelay);
            _roll.Compose(_animation);
            _inventory.Compose(_coinsLabel, _bombsLabel);
            _input.Compose(_rotationInput, _crouchInput, _rollInput, _attackInput, _collectInput, transform, _machine, _rotation, _inventory);
            _container.Compose();
        }

        private void ConfigurateObjects()
        {
            _rotation.Rotate(transform.forward);
            _ragdoll.Disable();
            _machine.StartState();
            _input.Enable();
            _container.Add(_animation);
            _container.Add(_physics);
            _container.Add(_machine);
            _container.Add(_health);
            _container.Add(_attack);
            _container.Add(_inventory);
        }
    }
}