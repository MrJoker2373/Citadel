namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;

    public class PlayerHealthView : HealthView
    {
        private const string HIDE_ANIMATION = "RowHide";
        private const string SHOW_ANIMATION = "RowShow";
        private const float CROSS_FADE = 0.125f;
        private const int HEART_COUNT = 12;
        [SerializeField] private GameObject[] _hearts;
        [SerializeField] private Animator _heartRow1;
        [SerializeField] private Animator _heartRow2;

        public override IEnumerator Refresh(float health, float capacity)
        {
            var hearts = Mathf.RoundToInt(HEART_COUNT * health / capacity);
            if (hearts >= 6)
                yield return StartCoroutine(_heartRow2.PlayAsync(HIDE_ANIMATION, CROSS_FADE));
            else
                yield return StartCoroutine(_heartRow1.PlayAsync(HIDE_ANIMATION, CROSS_FADE));
            for (int i = 0; i < HEART_COUNT; i++)
            {
                if (hearts - 1 >= i)
                    _hearts[i].SetActive(true);
                else
                    _hearts[i].SetActive(false);
            }
            if (hearts >= 6)
                yield return StartCoroutine(_heartRow2.PlayAsync(SHOW_ANIMATION, CROSS_FADE));
            else
                yield return StartCoroutine(_heartRow1.PlayAsync(SHOW_ANIMATION, CROSS_FADE));
        }
    }
}