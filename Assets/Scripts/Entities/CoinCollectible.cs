namespace Citadel.Unity.Entities
{
    using UnityEngine;
    using Citadel.Unity.Entities.Unit;
    public class CoinCollectible : MonoBehaviour, ICollectible
    {
        public void Collect()
        {
            Destroy(gameObject);
        }
        public bool IsPassive()
        {
            return true;
        }
        public bool IsType(ICollectible.CollectibleType type)
        {
            return type == ICollectible.CollectibleType.Coin;
        }
    }
}