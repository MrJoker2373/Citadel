namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Linq;

    public class UnitMachine
    {
        private UnitIdle _idle;
        private UnitMovement _movement;
        private UnitAttack _attack;
        private UnitRoll _roll;
        private UnitDeath _death;
        private IUnitState _currentState;
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
            _currentState = _currentDefault = _idle;
            _currentSpecial = _attack;
            _currentDefault.Start();
        }

        public void IdleState()
        {
            if (_currentState is IDeathState)
                return;
            if (_currentDefault is UnitIdle)
                return;
            _currentDefault = _idle;
            if (_currentState is IDefaultState)
            {
                _currentState.Stop();
                _currentState = _currentDefault;
                _currentDefault.Start();
            }
        }

        public void MovementState()
        {
            if (_currentState is IDeathState)
                return;
            if (_currentDefault is UnitMovement)
                return;
            _currentDefault = _movement;
            if (_currentState is IDefaultState)
            {
                _currentState.Stop();
                _currentState = _currentDefault;
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
                _currentSpecial.Start();
            else if (_currentState is not ISpecialState)
            {
                _currentState.Stop();
                _currentState = _currentSpecial = _attack;
                await _currentSpecial.Start();
                if (_currentState is not IDeathState)
                {
                    _currentState = _currentDefault;
                    _currentDefault.Start();
                }
            }
        }

        public async void RollState()
        {
            if (_currentState is IDeathState)
                return;
            if (_currentState is not ISpecialState)
            {
                _currentState.Stop();
                _currentState = _currentSpecial = _roll;
                await _currentSpecial.Start();
                if (_currentState is not IDeathState)
                {
                    _currentState = _currentDefault;
                    _currentDefault.Start();
                }
            }
        }

        public void DeathState()
        {
            _currentState.Stop();
            _currentState = _death;
            _death.Start();
        }
    }
}