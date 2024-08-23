namespace Citadel.Units
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class UnitAnimation
    {
        private Animator _animator;
        private string _animation;
        private bool _isPlay;
        private bool _isStart;
        private bool _isStop;
        private bool _isActive;

        public void Compose(Animator animator)
        {
            _animator = animator;
        }

        public void Enable()
        {
            _isActive = false;
            _animator.enabled = true;
        }

        public void Disable()
        {
            _isActive = true;
            _animator.enabled = false;
            Stop();
        }

        public async Task Play(string animation)
        {
            if (_isActive == false)
            {
                _animation = animation;
                await WaitForStop();
                _isPlay = true;
                await WaitForStart();
                await WaitForEnd();
                _isPlay = false;
            }
        }

        public void Update()
        {
            if (_isStart == true)
            {
                _animator.Play(_animation);
                _isStart = false;
            }
        }

        public void Stop()
        {
            _isStop = true;
        }

        private async Task WaitForStop()
        {
            if (_isPlay == false)
                _isStop = false;
            else
            {
                Stop();
                while (_isPlay == true)
                    await Task.Yield();
                _isStop = false;
            }
        }

        private async Task WaitForStart()
        {
            _isStart = true;
            while (_isStop == false && _isStart == true)
                await Task.Yield();
            if (_isStop == false)
                await Task.Delay((int)(Time.deltaTime * 1000 + 5));
        }

        private async Task WaitForEnd()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).loop == false)
            {
                float time = 0;
                while (_isStop == false && time < 0.99f)
                {
                    time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    await Task.Yield();
                }
            }
        }
    }
}