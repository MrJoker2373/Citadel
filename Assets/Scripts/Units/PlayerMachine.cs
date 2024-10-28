namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(MovementState))]
    [RequireComponent(typeof(AttackState))]
    [RequireComponent(typeof(DeathState))]
    [RequireComponent(typeof(RollState))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Inventory))]
    public class PlayerMachine : MonoBehaviour
    {
        private MovementState _movement;
        private AttackState _attack;
        private DeathState _death;
        private RollState _roll;
        private Health _health;
        private Inventory _inventory;
        private object _currentState;

        public bool IsDead => _death.IsDead;

        private void Awake()
        {
            _movement = GetComponent<MovementState>();
            _attack = GetComponent<AttackState>();
            _death = GetComponent<DeathState>();
            _roll = GetComponent<RollState>();
            _health = GetComponent<Health>();
            _inventory = GetComponent<Inventory>();
            _health.OnDeath += OnDeath;
        }

        private void Start()
        {
            _currentState = _movement;
            _movement.Enter();
        }

        public void Idle()
        {
            if (_currentState is not DeathState)
                _movement.Idle();
        }

        public void Move()
        {
            if (_currentState is not DeathState)
                _movement.Move();
        }

        public void Rotate(Vector3 direction)
        {
            if (_currentState is not DeathState)
                _movement.Rotate(direction);
        }

        public void Stand()
        {
            if (_currentState is not DeathState)
                _movement.Stand();
        }

        public void Crouch()
        {
            if (_currentState is not DeathState)
                _movement.Crouch();
        }

        public IEnumerator Attack()
        {
            if (_currentState is DeathState)
                yield break;
            if (_currentState is AttackState)
                StartCoroutine(_attack.Attack());
            else if (_currentState is MovementState)
            {
                _movement.Exit();
                _currentState = _attack;
                yield return StartCoroutine(_attack.Attack());
                if (_currentState is not DeathState)
                {
                    _currentState = _movement;
                    _movement.Enter();
                }
            }
        }

        public IEnumerator Roll()
        {
            if (_currentState is DeathState)
                yield break;
            if (_currentState is MovementState)
            {
                _movement.Exit();
                _currentState = _roll;
                yield return StartCoroutine(_roll.Roll());
                if (_currentState is not DeathState)
                {
                    _currentState = _movement;
                    _movement.Enter();
                }
            }
        }

        public void Collect()
        {
            _inventory.Collect();
        }

        private void OnDeath(Vector3 force)
        {
            if (_currentState is not DeathState)
            {
                _currentState = _death;
                _death.Die(force);
            }
        }
    }
}