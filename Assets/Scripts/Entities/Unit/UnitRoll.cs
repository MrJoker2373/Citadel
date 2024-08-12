namespace Citadel.Unity.Entities.Unit
{
    using System.Threading.Tasks;
    using Citadel.Unity.Core;
    public class UnitRoll
    {
        private readonly AnimationController _animation;
        public UnitRoll(AnimationController animation)
        {
            _animation = animation;
        }
        public async Task ProcessState()
        {
            const string key = "Roll";
            await _animation.Play(key);
        }
    }
}