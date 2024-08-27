namespace Citadel.Units
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class UnitAnimation
    {
        private const float CROSS_FADE = 0.175f;
        private const int THREAD_DELAY = 1000;
        private Animator _animator;
        private bool _isActive;

        public void Compose(Animator animator)
        {
            _animator = animator;
        }

        public void Enable()
        {
            _isActive = true;
            _animator.enabled = true;
        }

        public void Disable()
        {
            _isActive = false;
            _animator.enabled = false;
        }

        public async Task Play(string animation)
        {
            if (_isActive == false)
                return;
            _animator.CrossFadeInFixedTime(animation, CROSS_FADE);
            await Task.Delay((int)(CROSS_FADE * THREAD_DELAY) + 10);
            if (_animator.GetCurrentAnimatorStateInfo(0).loop == false)
            {
                while (_isActive == true && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
                    await Task.Yield();
            }
        }
    }
}