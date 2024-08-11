namespace Citadel.Unity.Units
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using TMPro;
    using Citadel.Unity.Core;
    using Citadel.Unity.Units.Components;
    public class PlayerBehaviour : MonoBehaviour, IUnitContainer
    {
        [Header(nameof(AnimationController))]
        [SerializeField] private Animator _animator;
        [Header(nameof(UnitMovement))]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [Header(nameof(UnitAttack))]
        [SerializeField] private int _damageAmount;
        [Header(nameof(UnitInventory))]
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _bombsLabel;
        [Header(nameof(UnitHealth))]
        [SerializeField] private int _healthAmount;
        [Header(nameof(UnitInput))]
        [SerializeField] private InputAction _movementInput;
        [SerializeField] private InputAction _rollInput;
        [SerializeField] private InputAction _attackInput;
        [SerializeField] private InputAction _collectInput;
        [SerializeField] private OrientationController _orientation;
        private AnimationController _animation;
        private UnitMovement _movement;
        private UnitRoll _roll;
        private UnitAttack _attack;
        private UnitInventory _inventory;
        private UnitHealth _health;
        private UnitInput _input;
        private List<object> _container;
        private void Awake()
        {
            InitializeComponents();
            InitializeContainer();
            _movement.EnterState();
        }
        private void FixedUpdate()
        {
            _movement.UpdateMovement();
            _movement.UpdateRotation();
        }
        private void LateUpdate()
        {
            _animation.Update();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICollectible>(out var collectible))
                _input.SetCollectible(collectible);
            else if (other.TryGetComponent<IUnitContainer>(out var container))
                _attack.SetHealth(container.GetUnitComponent<UnitHealth>());
        }
        private void InitializeComponents()
        {
            _animation = new(_animator);
            _movement = new(_animation, _rigidbody, _movementSpeed, _rotationSpeed);
            _roll = new(_animation);
            _attack = new(_animation, _damageAmount);
            _inventory = new(_coinsLabel, _bombsLabel);
            _health = new(gameObject, _healthAmount);
            _input = new(_movementInput, _rollInput, _attackInput, _collectInput, _orientation, _movement, _roll, _attack, _inventory);
        }
        private void InitializeContainer()
        {
            _container = new()
            {
                _animation,
                _movement,
                _roll,
                _attack,
                _inventory,
                _health,
                _input
            };
        }
        public T GetUnitComponent<T>() where T : class
        {
            return _container.OfType<T>().SingleOrDefault();
        }
    }
}