namespace Citadel
{
    using UnityEngine;

    [RequireComponent(typeof(UnitMachine))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;
        private UnitMachine _machine;

        private void Awake()
        {
            _machine = GetComponent<UnitMachine>();
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
                    _machine.Die(knockback);
                }
            }
        }
    }
}