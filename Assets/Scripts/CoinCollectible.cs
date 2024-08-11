namespace Citadel.Unity
{
    using UnityEngine;
    using Citadel.Unity.Units.Components;
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