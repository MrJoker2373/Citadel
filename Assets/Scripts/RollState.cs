namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(AnimationPlayer))]
    public class RollState : MonoBehaviour
    {
        private const string ROLL_ANIMATION = "Roll";
        private AnimationPlayer _animation;

        private void Awake()
        {
            _animation = GetComponent<AnimationPlayer>();
        }

        public IEnumerator Roll()
        {
            yield return _animation.PlayAsync(ROLL_ANIMATION);
        }
    }
}