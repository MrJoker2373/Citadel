namespace Citadel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(AnimationPlayer))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Health))]
    public class AttackState : MonoBehaviour
    {
        private const string ATTACK1_ANIMATION = "Attack 1";
        private const string ATTACK2_ANIMATION = "Attack 2";
        private const string ATTACK3_ANIMATION = "Attack 3";
        private const float DAMAGE_MULTIPLIER = 1.1f;
        private const int MAX_COMBO = 2;
        [SerializeField] private int _defaultDamage;
        private AnimationPlayer _animation;
        private Rigidbody _rigidbody;
        private Health _health;
        private List<Health> _hits;
        private int _currentDamage;
        private int _currentCombo;
        private bool _isAttack;
        private bool _isCombo;

        private void Awake()
        {
            _animation = GetComponent<AnimationPlayer>();
            _rigidbody = GetComponent<Rigidbody>();
            _health = GetComponent<Health>();
            _hits = new();
        }

        private void Start()
        {
            _currentDamage = _defaultDamage;
        }

        public void SetHit(Health hit)
        {
            if (_isAttack == false)
                return;
            if (_health != hit && _hits.Contains(hit) == false)
            {
                _hits.Add(hit);
                var knockback = _rigidbody.transform.forward * _currentDamage;
                hit.Damage(_currentDamage, knockback);
            }
        }

        public IEnumerator Attack()
        {
            if (_isAttack == false)
            {
                _isAttack = true;
                _hits.Clear();
                if (_currentCombo == 0)
                    yield return _animation.PlayAsync(ATTACK1_ANIMATION);
                else if (_currentCombo == 1)
                    yield return _animation.PlayAsync(ATTACK2_ANIMATION);
                else if (_currentCombo == 2)
                    yield return _animation.PlayAsync(ATTACK3_ANIMATION);
                _isAttack = false;
                if (_isCombo == false)
                {
                    _currentCombo = 0;
                    _currentDamage = _defaultDamage;
                }
                else
                {
                    _isCombo = false;
                    yield return CoroutineLauncher.Launch(Attack());
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
    }
}