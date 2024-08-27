namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class UnitMachine
    {
        private UnitIdle _idle;
        private UnitMovement _movement;
        private UnitAttack _attack;
        private UnitRoll _roll;
        private UnitDeath _death;
        private IUnitState _currentState;
        private IDefaultState _lastDefault;
        private IDefaultState _currentDefault;
        private ISpecialState _currentSpecial;
        private List<IUnitState> _allStates;

        public T GetState<T>() where T : IUnitState
        {
            return _allStates.OfType<T>().Single();
        }

        public IUnitState GetCurrentState()
        {
            return _currentState;
        }

        public void Compose(
            UnitIdle idle,
            UnitMovement movement,
            UnitAttack attack,
            UnitRoll roll,
            UnitDeath death)
        {
            _idle = idle;
            _movement = movement;
            _attack = attack;
            _roll = roll;
            _death = death;
            _allStates = new List<IUnitState>()
            {
                _idle,
                _movement,
                _attack,
                _roll,
                _death
            };
        }

        public void StartState()
        {
            _currentState = _currentDefault = _lastDefault = _idle;
            _currentSpecial = _attack;
            _currentDefault.Start();
        }

        public void IdleState()
        {
            if (_currentState is IDeathState)
                return;
            if (_lastDefault is UnitIdle)
                return;
            _lastDefault = _idle;
            if (_currentState is IDefaultState)
            {
                _currentDefault.Stop();
                _currentState = _currentDefault = _lastDefault;
                _currentDefault.Start();
            }
        }

        public void MovementState()
        {
            if (_currentState is IDeathState)
                return;
            if (_lastDefault is UnitMovement)
                return;
            _lastDefault = _movement;
            if (_currentState is IDefaultState)
            {
                _currentDefault.Stop();
                _currentState = _currentDefault = _lastDefault;
                _currentDefault.Start();
            }
        }

        public void DefaultState()
        {
            if (_currentState is IDeathState)
                return;
            _idle.Default();
            _movement.Default();
        }

        public void CrouchState()
        {
            if (_currentState is IDeathState)
                return;
            _idle.Crouch();
            _movement.Crouch();
        }

        public async void AttackState()
        {
            if (_currentState is IDeathState)
                return;
            if (_currentState is UnitAttack)
                _currentSpecial.Run();
            else if (_currentState is IDefaultState state)
            {
                state.Stop();
                _currentState = _currentSpecial = _attack;
                await _currentSpecial.Run();
                if (_currentState is not IDeathState)
                {
                    _currentState = _currentDefault = _lastDefault;
                    _currentDefault.Start();
                }
            }
        }

        public async void RollState()
        {
            if (_currentState is IDeathState)
                return;
            if (_currentState is IDefaultState state)
            {
                state.Stop();
                _currentState = _currentSpecial = _roll;
                await _currentSpecial.Run();
                if (_currentState is not IDeathState)
                {
                    _currentState = _currentDefault = _lastDefault;
                    _currentDefault.Start();
                }
            }
        }

        public void DeathState()
        {
            if (_currentState is IDefaultState state)
                state.Stop();
            _currentState = _death;
            _death.Run();
        }
    }
}