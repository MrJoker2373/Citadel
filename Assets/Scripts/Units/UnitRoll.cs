namespace Citadel.Units
{
    using System.Threading.Tasks;

    public class UnitRoll : ISpecialState
    {
        private const string ROLL_ANIMATION = "Roll";
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

        public async Task Start()
        {
            _isActive = true;
            await _animation.Play(ROLL_ANIMATION);
            Stop();
        }

        public void Stop()
        {
            _isActive = false;
            _animation.Stop();
        }
    }
}