namespace Citadel.Unity.Units.Components
{
    using UnityEngine;
    public class UnitHealth
    {
        private readonly GameObject _root;
        private int _health;
        public UnitHealth(GameObject root, int health)
        {
            _root = root;
            _health = health;
        }
        public void RemoveHealth(int health)
        {
            if (_health > 0)
            {
                if (_health > health)
                    _health -= health;
                else
                    Kill();
            }
        }
        private void Kill()
        {
            _health = 0;
            Object.Destroy(_root);
        }
    }
}