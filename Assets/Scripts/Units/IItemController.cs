namespace Citadel.Units
{
    public interface IItemController
    {
        public void Dispose();

        public bool IsPassive();

        public bool IsType(ItemType type);

        public enum ItemType
        {
            Coin,
            Bomb
        }
    }
}