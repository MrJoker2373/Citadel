namespace Citadel.Units
{
    using TMPro;

    public class UnitInventory
    {
        private TextMeshProUGUI _coinsLabel;
        private TextMeshProUGUI _bombsLabel;
        private IItemController _item;
        private int _coins;
        private int _bombs;

        public void Compose(TextMeshProUGUI coinsLabel, TextMeshProUGUI bombsLabel)
        {
            _coinsLabel = coinsLabel;
            _bombsLabel = bombsLabel;
        }

        public void SetItem(IItemController item)
        {
            _item = item;
            if (_item.IsPassive() == true)
                CollectItem();
        }

        public void ResetItem(IItemController item)
        {
            if (item == _item)
                _item = null;
        }

        public void CollectItem()
        {
            if (_item == null)
                return;
            if (_item.IsType(IItemController.ItemType.Coin))
            {
                _coins++;
                _coinsLabel.text = _coins.ToString();
            }
            else if (_item.IsType(IItemController.ItemType.Bomb))
            {
                _bombs++;
                _bombsLabel.text = _bombs.ToString();
            }
            _item.Dispose();
            _item = null;
        }
    }
}