namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(MovementState))]
    [RequireComponent(typeof(AttackState))]
    [RequireComponent(typeof(DeathState))]
    [RequireComponent(typeof(Health))]
    public class EnemyMachine : MonoBehaviour
    {
        private MovementState _movement;
        private AttackState _attack;
        private DeathState _death;
        private Health _health;
        private object _currentState;

        public bool IsDead => _death.IsDead;

        private void Awake()
        {
            _movement = GetComponent<MovementState>();
            _attack = GetComponent<AttackState>();
            _death = GetComponent<DeathState>();
            _health = GetComponent<Health>();
            _health.OnDeath += OnDeath;
        }

        private void Start()
        {
            _currentState = _movement;
            _movement.Enter();
        }

        public void Idle()
        {
            if (_currentState is MovementState)
                _movement.Idle();
        }

        public void Move()
        {
            if (_currentState is MovementState)
                _movement.Move();
        }

        public void Rotate(Vector3 direction)
        {
            if (_currentState is MovementState)
                _movement.Rotate(direction);
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