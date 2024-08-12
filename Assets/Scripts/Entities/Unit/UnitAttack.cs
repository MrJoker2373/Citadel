namespace Citadel.Unity.Entities.Unit
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Citadel.Unity.Core;
    public class UnitAttack
    {
        private readonly AnimationController _animation;
        private readonly int _damage;
        private readonly List<UnitHealth> _hits;
        private bool _isRunning;
        public UnitAttack(AnimationController animation, int damage)
        {
            _animation = animation;
            _damage = damage;
            _hits = new();
        }
        public async Task ProcessState()
        {
            const string key = "Attack";
            _isRunning = true;
            await _animation.Play(key);
            _hits.Clear();
            _isRunning = false;
        }
        public void SetHealth(UnitHealth hit)
        {
            if (_isRunning == true)
            {
                if (_hits.Contains(hit) == false)
                {
                    _hits.Add(hit);
                    hit.RemoveHealth(_damage);
                }
            }
        }
    }
}