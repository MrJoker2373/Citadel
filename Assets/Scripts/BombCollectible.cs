namespace Citadel.Unity
{
    using UnityEngine;
    using Citadel.Unity.Units.Components;
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