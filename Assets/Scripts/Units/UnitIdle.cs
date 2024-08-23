namespace Citadel.Units
{
    public class UnitIdle : IDefaultState
    {
        private const string IDLE_ANIMATION = "Idle";
        private UnitAnimation _animation;
        private bool _isActive;

        public bool IsActive()
        {
            return _isActive;
        }

        public void Compose(UnitAnimation animation)
        {
            _animation = animation;
        }

        public async void Start()
        {
            _isActive = true;
            await _animation.Play(IDLE_ANIMATION);
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}