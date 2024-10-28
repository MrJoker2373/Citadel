namespace Citadel.Units
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(HealthView))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;
        private HealthView _view;
        private int _capacity;

        public event Action<Vector3> OnDeath;

        private void Awake()
        {
            _view = GetComponent<HealthView>();
            _capacity = _health;
        }

        public void Damage(int health, Vector3 knockback)
        {
            if (_health > 0)
            {
                if (_health > health)
                    _health -= health;
                else
                {
                    _health = 0;
                    OnDeath?.Invoke(knockback);
                }
                StartCoroutine(_view.Refresh(_health, _capacity));
            }
        }
    }
}