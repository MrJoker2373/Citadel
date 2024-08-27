namespace Citadel.Units
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class UnitDeath : IDeathState
    {
        private const float KNOCKBACK_MULTIPLIER = 1.7f;
        private UnitRagdoll _ragdoll;
        private UnitController _controller;
        private UnitPhysics _physics;
        private Vector3 _knockback;
        private int _delay;

        public void Compose(
            UnitRagdoll ragdoll,
            UnitController controller,
            int delay)
        {
            _ragdoll = ragdoll;
            _controller = controller;
            _delay = delay;
        }

        public void SetKnockback(Vector3 knockback)
        {
            _knockback = knockback;
        }

        public async void Run()
        {
            _ragdoll.Enable();
            _ragdoll.Push(_knockback * KNOCKBACK_MULTIPLIER);
            await Task.Delay(_delay);
            _controller.Dispose();
        }
    }
}