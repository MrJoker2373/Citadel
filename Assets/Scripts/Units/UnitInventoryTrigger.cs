namespace Citadel.Units
{
    using UnityEngine;

    public class UnitInventoryTrigger : MonoBehaviour
    {
        [SerializeField] private UnitContainer _container;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IItemController>(out var item))
                _container.Get<UnitInventory>().SetItem(item);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IItemController>(out var item))
                _container.Get<UnitInventory>().ResetItem(item);
        }
    }
}