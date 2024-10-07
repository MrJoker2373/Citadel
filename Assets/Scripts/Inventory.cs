﻿namespace Citadel
{
    using UnityEngine;
    using TMPro;

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _bombsLabel;
        private CollectibleItem _currentItem;
        private int _coins;
        private int _bombs;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CollectibleItem>(out var item))
            {
                _currentItem = item;
                if (_currentItem.IsPassive() == true)
                    CollectItem();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CollectibleItem>(out var item))
            {
                if (item == _currentItem)
                    _currentItem = null;
            }
        }

        public void CollectItem()
        {
            if (_currentItem == null)
                return;
            if (_currentItem.IsType(CollectibleItem.ItemType.Coin))
            {
                _coins++;
                _coinsLabel.text = _coins.ToString();
            }
            else if (_currentItem.IsType(CollectibleItem.ItemType.Bomb))
            {
                _bombs++;
                _bombsLabel.text = _bombs.ToString();
            }
            _currentItem.Collect();
            _currentItem = null;
        }
    }
}