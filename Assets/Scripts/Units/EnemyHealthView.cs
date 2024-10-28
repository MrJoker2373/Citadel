namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyHealthView : HealthView
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;

        public override IEnumerator Refresh(float health, float capacity)
        {
            _image.fillAmount = health / capacity;
            _canvas.enabled = health > 0;
            yield return null;
        }
    }
}