namespace Citadel.Unity.Entities.Unit
{
    public interface ICollectible
    {
        public void Collect();
        public bool IsPassive();
        public bool IsType(CollectibleType type);
        public enum CollectibleType
        {
            Coin,
            Bomb
        }
    }
}