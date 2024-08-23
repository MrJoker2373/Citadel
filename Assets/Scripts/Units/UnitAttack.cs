namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UnitAttack : ISpecialState
    {
        private const string ATTACK_ANIMATION = "Attack";
        private UnitAnimation _animation;
        private UnitHealth _health;
        private int _damage;
        private List<UnitHealth> _hits;
        private bool _isActive;

        public bool IsActive()
        {
            return _isActive;
        }

        public void Compose(UnitAnimation animation, UnitHealth health, int damage)
        {
            _animation = animation;
            _health = health;
            _damage = damage;
            _hits = new();
        }

        public async Task Start()
        {
            _isActive = true;
            await _animation.Play(ATTACK_ANIMATION);
            Stop();
        }

        public void Stop()
        {
            _isActive = false;
            _animation.Stop();
            _hits.Clear();
        }

        public void SetHit(UnitHealth hit)
        {
            if (_isActive == false)
                return;
            if (_health != hit && _hits.Contains(hit) == false)
            {
                _hits.Add(hit);
                hit.RemoveHealth(_damage);
            }
        }
    }
}