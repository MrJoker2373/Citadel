namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(MovementState))]
    [RequireComponent(typeof(AttackState))]
    [RequireComponent(typeof(RollState))]
    [RequireComponent(typeof(DeathState))]
    [RequireComponent(typeof(Inventory))]
    public class PlayerMachine : UnitMachine
    {
        private MovementState _movementState;
        private AttackState _attackState;
        private RollState _rollState;
        private DeathState _deathState;
        private Inventory _inventory;
        private object _currentState;

        private void Awake()
        {
            _movementState = GetComponent<MovementState>();
            _attackState = GetComponent<AttackState>();
            _rollState = GetComponent<RollState>();
            _deathState = GetComponent<DeathState>();
            _inventory = GetComponent<Inventory>();
        }

        private void Start()
        {
            _currentState = _movementState;
            _movementState.Enter();
        }

        public void Idle()
        {
            if (_currentState is not DeathState)
                _movementState.Idle();
        }

        public void Move()
        {
            if (_currentState is not DeathState)
                _movementState.Move();
        }

        public void Rotate(Vector3 direction)
        {
            if (_currentState is not DeathState)
                _movementState.Rotate(direction);
        }

        public void Stand()
        {
            if (_currentState is not DeathState)
                _movementState.Stand();
        }

        public void Crouch()
        {
            if (_currentState is not DeathState)
                _movementState.Crouch();
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

        public IEnumerator Roll()
        {
            if (_currentState is DeathState)
                yield break;
            if (_currentState is MovementState)
            {
                _movementState.Exit();
                _currentState = _rollState;
                yield return CoroutineLauncher.Launch(_rollState.Roll());
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

        public void Collect()
        {
            _inventory.CollectItem();
        }
    }
}