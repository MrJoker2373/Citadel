namespace Citadel.Unity.Units.Components
{
    using TMPro;
    public class UnitInventory
    {
        private readonly TextMeshProUGUI _coinsLabel;
        private readonly TextMeshProUGUI _bombsLabel;
        private int _coins;
        private int _bombs;
        public UnitInventory(TextMeshProUGUI coinsLabel, TextMeshProUGUI bombsLabel)
        {
            _coinsLabel = coinsLabel;
            _bombsLabel = bombsLabel;
        }
        public void AddCoin()
        {
            _coins++;
            _coinsLabel.text = _coins.ToString();
        }
        public void AddBomb()
        {
            _bombs++;
            _bombsLabel.text = _bombs.ToString();
        }
    }
}