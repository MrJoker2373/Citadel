namespace Citadel.Units
{
    using UnityEngine;

    public class UnitHealth
    {
        private UnitMachine _machine;
        private int _health;

        public void Compose(UnitMachine machine, int health)
        {
            _machine = machine;
            _health = health;
        }

        public void RemoveHealth(UnitPhysics physics, int health)
        {
            if (_health > 0)
            {
                if (_health > health)
                    _health -= health;
                else
                {
                    _health = 0;
                    _machine.GetState<UnitDeath>().SetPhysics(physics);
                    _machine.DeathState();
                }
            }
        }
    }
}