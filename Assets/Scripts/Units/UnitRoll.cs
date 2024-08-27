namespace Citadel.Units
{
    using System.Threading.Tasks;

    public class UnitRoll : ISpecialState
    {
        private const string ROLL_KEY = "Roll";
        private UnitAnimation _animation;

        public void Compose(UnitAnimation animation)
        {
            _animation = animation;
        }

        public async Task Run()
        {
            await _animation.Play(ROLL_KEY);
        }
    }
}