namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class RollState : MonoBehaviour
    {
        private const float CROSS_FADE = 0.125f;
        private const string ROLL_ANIMATION = "Roll";
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public IEnumerator Roll()
        {
            yield return _animator.PlayAsync(ROLL_ANIMATION, CROSS_FADE);
        }
    }
}