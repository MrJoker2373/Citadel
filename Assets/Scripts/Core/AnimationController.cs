namespace Citadel.Unity.Core
{
    using System.Threading.Tasks;
    using UnityEngine;
    public class AnimationController
    {
        private readonly Animator _animator;
        private string _animation;
        private bool _isPlaying;
        private bool _isStarting;
        private bool _isStopping;
        public AnimationController(Animator animator)
        {
            _animator = animator;
        }
        public void Update()
        {
            if (_isStarting == true)
            {
                _animator.Play(_animation);
                _isStarting = false;
            }
        }
        public async Task Play(string animation)
        {
            _animation = animation;
            await Stop();
            _isPlaying = true;
            await WaitForStart();
            await WaitForEnd();
            _isPlaying = false;
        }
        public async Task Stop()
        {
            if (_isPlaying == true)
            {
                _isStopping = true;
                while (_isPlaying == true)
                    await Task.Yield();
                _isStopping = false;
            }
        }
        private async Task WaitForStart()
        {
            _isStarting = true;
            while (_isStopping == false && _isStarting == true)
                await Task.Yield();
            if (_isStopping == false)
                await Task.Delay((int)(Time.deltaTime * 1000 + 5));
        }
        private async Task WaitForEnd()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).loop == false)
            {
                const float threshold = 0.99f;
                float time = 0;
                while (_isStopping == false && time < threshold)
                {
                    time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    await Task.Yield();
                }
            }
        }
    }
}