namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UnitAttack : ISpecialState
    {
        private const string ATTACK1_KEY = "Attack 1";
        private const string ATTACK2_KEY = "Attack 2";
        private const string ATTACK3_KEY = "Attack 3";
        private const int MAX_COMBO = 2;
        private UnitAnimation _animation;
        private UnitHealth _health;
        private int _damage;
        private List<UnitHealth> _hits;
        private int _currentCombo;
        private bool _isAttack;
        private bool _isCombo;

        public void Compose(UnitAnimation animation, UnitHealth health, int damage)
        {
            _animation = animation;
            _health = health;
            _damage = damage;
            _hits = new();
        }

        public async Task Start()
        {
            if (_isAttack == false)
            {
                _isAttack = true;
                await SetAnimation();
                if (_isCombo == false)
                {
                    _currentCombo = 0;
                    Stop();
                }
                else
                {
                    Stop();
                    await Start();
                }
            }
            else if (_isCombo == false)
            {
                if (_currentCombo < MAX_COMBO)
                {
                    _isCombo = true;
                    _currentCombo++;
                }
            }
        }

        public void Stop()
        {
            _isAttack = false;
            _isCombo = false;
            _hits.Clear();
            _animation.Stop();
        }

        public void SetHit(UnitHealth hit)
        {
            if (_isAttack == false)
                return;
            if (_health != hit && _hits.Contains(hit) == false)
            {
                _hits.Add(hit);
                hit.RemoveHealth(_damage);
            }
        }

        private async Task SetAnimation()
        {
            if (_currentCombo == 0)
                await _animation.Play(ATTACK1_KEY);
            else if (_currentCombo == 1)
                await _animation.Play(ATTACK2_KEY);
            else if (_currentCombo == 2)
                await _animation.Play(ATTACK3_KEY);
        }
    }
}