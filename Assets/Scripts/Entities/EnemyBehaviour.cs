namespace Citadel.Unity.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Citadel.Unity.Core;
    using Citadel.Unity.Entities.Unit;
    public class EnemyBehaviour : MonoBehaviour, IUnitContainer
    {
        [Header(nameof(AnimationController))]
        [SerializeField] private Animator _animator;
        [Header(nameof(UnitMovement))]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [Header(nameof(UnitAttack))]
        [SerializeField] private int _damageAmount;
        [Header(nameof(UnitDeath))]
        [SerializeField] private GameObject _root;
        [Header(nameof(UnitHealth))]
        [SerializeField] private int _healthAmount;
        [Header(nameof(UnitNavigation))]
        [SerializeField] private Transform _target;
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _stopRange;
        private AnimationController _animation;
        private UnitMovement _movement;
        private UnitAttack _attack;
        private UnitDeath _death;
        private UnitHealth _health;
        private UnitNavigation _navigation;
        private List<object> _container;
        private void Awake()
        {
            InitializeComponents();
            InitializeContainer();
            _movement.EnterState();
        }
        private void Update()
        {
            _navigation.Update();
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
            if (other.TryGetComponent<IUnitContainer>(out var container))
                _attack.SetHealth(container.GetUnitComponent<UnitHealth>());
        }
        private void InitializeComponents()
        {
            _animation = new(_animator);
            _movement = new(_animation, _rigidbody, _movementSpeed, _rotationSpeed);
            _attack = new(_animation, _damageAmount);
            _death = new(_animation, _root);
            _health = new(_death, _healthAmount);
            _navigation = new(_movement, _attack, _target, _chaseRange, _stopRange);
        }
        private void InitializeContainer()
        {
            _container = new()
            {
                _animation,
                _movement,
                _attack,
                _death,
                _health,
                _navigation
            };
        }
        public T GetUnitComponent<T>() where T : class
        {
            return _container.OfType<T>().SingleOrDefault();
        }
    }
}