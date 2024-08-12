namespace Citadel.Unity.Entities
{
    using UnityEngine;
    using Citadel.Unity.Entities.Unit;
    public class BombCollectible : MonoBehaviour, ICollectible
    {
        public void Collect()
        {
            Destroy(gameObject);
        }
        public bool IsPassive()
        {
            return false;
        }
        public bool IsType(ICollectible.CollectibleType type)
        {
            return type == ICollectible.CollectibleType.Bomb;
        }
    }
}