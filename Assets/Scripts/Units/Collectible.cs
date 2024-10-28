namespace Citadel.Units
{
    using UnityEngine;

    public class Collectible : MonoBehaviour
    {
        [SerializeField] private CollectibleType _type;
        [SerializeField] private bool _isPassive;

        public CollectibleType Type => _type;

        public bool IsPassive => _isPassive;

        public void Collect() => Destroy(gameObject);

        public enum CollectibleType
        {
            Coin,
            Key,
            Potion
        }
    }
}