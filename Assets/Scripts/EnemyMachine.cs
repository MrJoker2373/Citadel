namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(MovementState))]
    [RequireComponent(typeof(AttackState))]
    [RequireComponent(typeof(DeathState))]
    public class EnemyMachine : UnitMachine
    {
        private MovementState _movementState;
        private AttackState _attackState;
        private DeathState _deathState;
        private object _currentState;

        private void Awake()
        {
            _movementState = GetComponent<MovementState>();
            _attackState = GetComponent<AttackState>();
            _deathState = GetComponent<DeathState>();
        }

        private void Start()
        {
            _currentState = _movementState;
            _movementState.Enter();
        }

        public void Idle()
        {
            if (_currentState is MovementState)
                _movementState.Idle();
        }

        public void Move()
        {
            if (_currentState is MovementState)
                _movementState.Move();
        }

        public void Rotate(Vector3 direction)
        {
            if (_currentState is MovementState)
                _movementState.Rotate(direction);
        }

        public IEnumerator Attack()
        {
            if (_currentState is DeathState)
                yield break;
            if (_currentState is AttackState)
                CoroutineLauncher.Launch(_attackState.Attack());
            else if (_currentState is MovementState)
            {
                _movementState.Exit();
                _currentState = _attackState;
                yield return StartCoroutine(_attackState.Attack());
                if (_currentState is not DeathState)
                {
                    _currentState = _movementState;
                    _movementState.Enter();
                }
            }
        }

        public override bool IsDead()
        {
            return _deathState.IsDead;
        }

        public override void Die(Vector3 force)
        {
            if (_currentState is not DeathState)
            {
                _currentState = _deathState;
                CoroutineLauncher.Launch(_deathState.Die(force));
            }
        }
    }
}