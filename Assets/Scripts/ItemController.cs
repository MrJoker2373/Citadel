namespace Citadel
{
    using UnityEngine;
    using Citadel.Units;

    public class ItemController : MonoBehaviour, IItemController
    {
        [SerializeField] private IItemController.ItemType _type;
        [SerializeField] private bool _isPassive;

        public void Dispose() => Destroy(gameObject);

        public bool IsPassive() => _isPassive;

        public bool IsType(IItemController.ItemType type) => _type == type;
    }
}