namespace Citadel.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody))]
    public class AttackState : MonoBehaviour
    {
        private const string ATTACK1_ANIMATION = "Attack 1";
        private const string ATTACK2_ANIMATION = "Attack 2";
        private const string ATTACK3_ANIMATION = "Attack 3";
        private const float CROSS_FADE = 0.125f;
        private const float DAMAGE_MULTIPLIER = 1.1f;
        private const int MAX_COMBO = 2;
        [SerializeField] private int _defaultDamage;
        private Animator _animator;
        private Health _health;
        private Rigidbody _rigidbody;
        private List<Health> _hits;
        private int _currentDamage;
        private int _currentCombo;
        private bool _isAttack;
        private bool _isCombo;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _rigidbody = GetComponent<Rigidbody>();
            _hits = new();
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
                    yield return _animator.PlayAsync(ATTACK1_ANIMATION, CROSS_FADE);
                else if (_currentCombo == 1)
                    yield return _animator.PlayAsync(ATTACK2_ANIMATION, CROSS_FADE);
                else if (_currentCombo == 2)
                    yield return _animator.PlayAsync(ATTACK3_ANIMATION, CROSS_FADE);
                _isAttack = false;
                if (_isCombo == false)
                {
                    _currentCombo = 0;
                    _currentDamage = _defaultDamage;
                }
                else
                {
                    _isCombo = false;
                    yield return StartCoroutine(Attack());
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