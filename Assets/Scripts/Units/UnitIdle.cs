namespace Citadel.Units
{
    public class UnitIdle : IDefaultState
    {
        private const string DEFAULT_KEY = "Default Idle";
        private const string CROUCH_KEY = "Crouch Idle";
        private UnitAnimation _animation;
        private bool _isIdle;
        private bool _isCrouch;

        public void Compose(UnitAnimation animation)
        {
            _animation = animation;
        }

        public void Start()
        {
            _isIdle = true;
            if (_isCrouch == false)
                _animation.Play(DEFAULT_KEY);
            else
                _animation.Play(CROUCH_KEY);
        }

        public void Stop()
        {
            _isIdle = false;
        }

        public void Default()
        {
            _isCrouch = false;
            if (_isIdle == true)
                _animation.Play(DEFAULT_KEY);
        }

        public void Crouch()
        {
            _isCrouch = true;
            if (_isIdle == true)
                _animation.Play(CROUCH_KEY);
        }
    }
}