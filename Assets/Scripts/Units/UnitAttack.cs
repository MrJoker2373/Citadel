namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UnitAttack : ISpecialState
    {
        private const string ATTACK1_KEY = "Attack 1";
        private const string ATTACK2_KEY = "Attack 2";
        private const string ATTACK3_KEY = "Attack 3";
        private const float DAMAGE_MULTIPLIER = 1.1f;
        private const int MAX_COMBO = 2;
        private UnitAnimation _animation;
        private UnitPhysics _physics;
        private UnitHealth _health;
        private int _defaultDamage;
        private int _currentDamage;
        private List<UnitHealth> _hits;
        private int _currentCombo;
        private bool _isAttack;
        private bool _isCombo;

        public void Compose(UnitAnimation animation, UnitPhysics physics, UnitHealth health, int damage)
        {
            _animation = animation;
            _physics = physics;
            _health = health;
            _defaultDamage = _currentDamage = damage;
            _hits = new();
        }

        public async Task Start()
        {
            if (_isAttack == false)
            {
                _isAttack = true;
                await SetAnimation();
                if (_isCombo == false)
                    Stop();
                else
                {
                    ResetAttack();
                    await Start();
                }
            }
            else if (_isCombo == false)
            {
                if (_currentCombo < MAX_COMBO)
                {
                    _isCombo = true;
                    _currentCombo++;
                    _currentDamage = (int)(_currentDamage * DAMAGE_MULTIPLIER);
                }
            }
        }

        public void Stop()
        {
            ResetAttack();
            _currentCombo = 0;
            _currentDamage = _defaultDamage;
            _animation.Stop();
        }

        public void SetHit(UnitHealth hit)
        {
            if (_isAttack == false)
                return;
            if (_health != hit && _hits.Contains(hit) == false)
            {
                _hits.Add(hit);
                hit.RemoveHealth(_physics, _currentDamage);
            }
        }

        private void ResetAttack()
        {
            _isAttack = false;
            _isCombo = false;
            _hits.Clear();
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