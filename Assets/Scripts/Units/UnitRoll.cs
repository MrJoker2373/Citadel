namespace Citadel.Units
{
    using System.Threading.Tasks;

    public class UnitRoll : ISpecialState
    {
        private const string ROLL_KEY = "Roll";
        private UnitAnimation _animation;
        private bool _isRoll;

        public void Compose(UnitAnimation animation)
        {
            _animation = animation;
        }

        public async Task Start()
        {
            if (_isRoll == false)
            {
                _isRoll = true;
                await _animation.Play(ROLL_KEY);
                Stop();
            }
        }

        public void Stop()
        {
            _isRoll = false;
            _animation.Stop();
        }
    }
}