namespace Citadel.Unity.Units.Components
{
    public class UnitHealth
    {
        private readonly UnitDeath _death;
        private int _health;
        public UnitHealth(UnitDeath death, int health)
        {
            _death = death;
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
            _death.Death();
        }
    }
}