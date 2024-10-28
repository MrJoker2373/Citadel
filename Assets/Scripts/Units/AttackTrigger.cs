namespace Citadel.Units
{
    using UnityEngine;

    public class AttackTrigger : MonoBehaviour
    {
        private AttackState _attack;

        private void Awake()
        {
            _attack = GetComponentInParent<AttackState>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Health>(out var hit))
                _attack.SetHit(hit);
        }
    }
}