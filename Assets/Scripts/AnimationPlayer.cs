namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Animator))]
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _fade;
        private Animator _animator;
        private bool _isActive;
        private bool _isPlaying;

        public float Speed
        {
            get => _speed;
            set
            {
                if (value >= 0)
                    _speed = value;
            }
        }

        public float Fade
        {
            get => _fade;
            set
            {
                if (value >= 0)
                    _fade = value;
            }
        }

        public bool IsPlaying => _isPlaying;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Enable();
        }

        public void Enable()
        {
            _isActive = true;
            _animator.enabled = true;
            _animator.applyRootMotion = true;
        }

        public void Disable()
        {
            _isActive = false;
            _animator.enabled = false;
            _animator.applyRootMotion = false;
        }

        public void Play(string animation)
        {
            if (_isActive == false || _isPlaying == true)
                return;
            _animator.speed = _speed;
            _animator.CrossFadeInFixedTime(animation, _fade);
        }

        public IEnumerator PlayAsync(string animation)
        {
            if (_isActive == false || _isPlaying == true)
                yield break;
            _isPlaying = true;
            _animator.speed = _speed;
            _animator.CrossFadeInFixedTime(animation, _fade);
            yield return new WaitForSeconds(_fade + 0.05f);
            while (_isActive == true && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
                yield return null;
            _isPlaying = false;
        }
    }
}