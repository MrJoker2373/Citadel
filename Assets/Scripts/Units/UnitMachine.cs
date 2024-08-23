namespace Citadel.Units
{
    using System.Linq;

    public class UnitMachine
    {
        private IDefaultState[] _defaultStates;
        private ISpecialState[] _specialStates;
        private IDeathState _deathState;
        private IUnitState _currentState;
        private IDefaultState _currentDefault;
        private ISpecialState _currentSpecial;

        public void Compose(
            IDefaultState[] defaultStates,
            ISpecialState[] specialStates,
            IDeathState deathState)
        {
            _defaultStates = defaultStates;
            _specialStates = specialStates;
            _deathState = deathState;
        }

        public IUnitState GetCurrentState()
        {
            return _currentState;
        }

        public void StartState()
        {
            _currentState = _currentDefault = _defaultStates[0];
            _currentSpecial = _specialStates[0];
            _currentDefault.Start();
        }

        public void DefaultState<T>() where T : IDefaultState
        {
            if (_currentState is IDefaultState)
                _currentDefault.Stop();
            _currentDefault = _defaultStates.OfType<T>().Single();
            if (_currentState is IDefaultState)
            {
                _currentState = _currentDefault;
                _currentDefault.Start();
            }
        }

        public async void SpecialState<T>() where T : ISpecialState
        {
            if (_currentState is not IDeathState)
            {
                if (_currentSpecial.IsActive() == false)
                {
                    _currentDefault.Stop();
                    _currentState = _currentSpecial = _specialStates.OfType<T>().Single();
                    await _currentSpecial.Start();
                    if (_currentState is not IDeathState)
                    {
                        _currentState = _currentDefault;
                        _currentDefault.Start();
                    }
                }
            }
        }

        public void DeathState()
        {
            if (_currentState is not IDeathState)
            {
                _currentDefault.Stop();
                _currentSpecial.Stop();
                _currentState = _deathState;
                _deathState.Start();
            }
        }
    }
}