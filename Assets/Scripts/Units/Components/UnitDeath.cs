namespace Citadel.Unity.Units.Components
{
    using UnityEngine;
    using Citadel.Unity.Core;
    public class UnitDeath
    {
        private readonly AnimationController _animation;
        private readonly GameObject _root;
        public UnitDeath(AnimationController animation, GameObject root)
        {
            _animation = animation;
            _root = root;
        }
        public async void Death()
        {
            await _animation.Stop();
            Object.Destroy(_root);
        }
    }
}