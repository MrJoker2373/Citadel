namespace Citadel
{
    using UnityEngine;

    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private bool _isPassive;

        public void Collect() => Destroy(gameObject);

        public bool IsPassive() => _isPassive;

        public bool IsType(ItemType type) => _type == type;

        public enum ItemType
        {
            Coin,
            Bomb
        }
    }
}