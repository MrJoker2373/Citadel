namespace Citadel.Units
{
    using UnityEngine;
    using TMPro;

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _keysLabel;
        [SerializeField] private TextMeshProUGUI _potionsLabel;
        private Collectible _currentCollectible;
        private int _coins;
        private int _keys;
        private int _potions;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collectible>(out var item))
            {
                _currentCollectible = item;
                if (_currentCollectible.IsPassive == true)
                    Collect();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Collectible>(out var item))
            {
                if (item == _currentCollectible)
                    _currentCollectible = null;
            }
        }

        public void Collect()
        {
            if (_currentCollectible == null)
                return;
            if (_currentCollectible.Type == Collectible.CollectibleType.Coin)
            {
                _coins++;
                _coinsLabel.text = _coins.ToString();
            }
            else if (_currentCollectible.Type == Collectible.CollectibleType.Key)
            {
                _keys++;
                _keysLabel.text = _keys.ToString();
            }
            else if (_currentCollectible.Type == Collectible.CollectibleType.Potion)
            {
                _potions++;
                _potionsLabel.text = _potions.ToString();
            }
            _currentCollectible.Collect();
            _currentCollectible = null;
        }
    }
}