namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Ragdoll))]
    public class DeathState : MonoBehaviour
    {
        [SerializeField] private float _delay;
        private Ragdoll _ragdoll;
        private bool _isDead;

        public bool IsDead => _isDead;

        private void Awake()
        {
            _ragdoll = GetComponent<Ragdoll>();
        }

        public void Die(Vector3 force)
        {
            _isDead = true;
            _ragdoll.Enable();
            _ragdoll.Rigidbody.velocity += force * 1.5f;
            Destroy(gameObject, _delay);
        }
    }
}