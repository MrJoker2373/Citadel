namespace Citadel.Units
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class UnitAnimation
    {
        private const float CROSS_FADE = 0.1f;
        private const int THREAD_DELAY = 100;
        private Animator _animator;
        private bool _isActive;
        private bool _isPlay;
        private bool _isStop;

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
            Stop();
        }

        public async Task Play(string animation)
        {
            if (_isActive == false)
                return;
            _isStop = true;
            await WaitForStop();
            _isPlay = true;
            await WaitForPlay(animation);
            _isPlay = false;
        }

        public void Stop()
        {
            _isStop = true;
        }

        private async Task WaitForPlay(string animation)
        {
            _animator.CrossFadeInFixedTime(animation, CROSS_FADE);
            await Task.Delay(THREAD_DELAY);
            if (_animator.GetCurrentAnimatorStateInfo(0).loop == false)
            {
                while (_isStop == false && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
                    await Task.Yield();
            }
        }

        private async Task WaitForStop()
        {
            while (_isPlay == true)
                await Task.Yield();
            _isStop = false;
        }
    }
}