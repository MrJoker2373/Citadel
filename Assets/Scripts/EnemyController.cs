namespace Citadel
{
    using UnityEngine;
    using Citadel.Units;

    public class EnemyController : UnitController
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _mainRigidbody;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Rigidbody _rootRigidbody;
        [SerializeField] private Collider _rootCollider;
        [SerializeField] private Rigidbody[] _ragdollRigidbodies;
        [SerializeField] private Collider[] _ragdollColliders;
        [SerializeField] private UnitContainer _container;
        [SerializeField] private UnitContainer _target;

        [SerializeField] private float _speedAmount;
        [SerializeField] private int _damageAmount;
        [SerializeField] private int _healthAmount;
        [SerializeField] private int _deathDelay;
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _stopRange;

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
        private UnitAgent _agent;

        private void Update()
        {
            _agent.Update();
        }

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
            _agent = new();
        }

        private void ComposeObjects()
        {
            _animation.Compose(_animator);
            _ragdoll.Compose(_animation, _mainRigidbody, _mainCollider, _rootRigidbody, _rootCollider, _ragdollRigidbodies, _ragdollColliders);
            _physics.Compose(_mainRigidbody);
            _rotation.Compose(_machine, _physics, _mainRigidbody);
            _machine.Compose(_idle, _movement, _attack, null, _death);
            _idle.Compose(_animation);
            _movement.Compose(_physics, _animation, _speedAmount, 0);
            _attack.Compose(_animation, _physics, _health, _damageAmount);
            _health.Compose(_machine, _healthAmount);
            _death.Compose(_ragdoll, this, _deathDelay);
            _agent.Compose(_machine, _physics, _rotation, _chaseRange, _stopRange);
            _container.Compose();
        }

        private void ConfigurateObjects()
        {
            _rotation.Rotate(transform.forward);
            _ragdoll.Disable();
            _machine.StartState();
            _agent.SetTarget(_target);
            _container.Add(_animation);
            _container.Add(_physics);
            _container.Add(_machine);
            _container.Add(_health);
            _container.Add(_attack);
        }
    }
}