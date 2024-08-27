namespace Citadel.Units
{
    using UnityEngine;

    public class UnitAttackTrigger : MonoBehaviour
    {
        [SerializeField] private UnitContainer _container;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<UnitContainer>(out var container))
                _container.Get<UnitAttack>().SetHit(container.Get<UnitHealth>());
        }
    }
}