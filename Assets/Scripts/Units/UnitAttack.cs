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

        public async Task Run()
        {
            if (_isAttack == false)
            {
                _isAttack = true;
                _hits.Clear();
                await SetAnimation();
                _isAttack = false;
                if (_isCombo == false)
                {
                    _isCombo = false;
                    _currentCombo = 0;
                    _currentDamage = _defaultDamage;
                }
                else
                {
                    _isCombo = false;
                    await Run();
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

        public void SetHit(UnitHealth hit)
        {
            if (_isAttack == false)
                return;
            if (_health != hit && _hits.Contains(hit) == false)
            {
                _hits.Add(hit);
                var knockback = _physics.GetDirection() * _currentDamage;
                hit.RemoveHealth(_currentDamage, knockback);
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