namespace Citadel.Units
{
    using System.Threading.Tasks;

    public class UnitDeath : IDeathState
    {
        private const float PUSH_FORCE = 50f;
        private UnitRagdoll _ragdoll;
        private UnitController _controller;
        private UnitPhysics _physics;
        private int _delay;
        private bool _isDead;

        public void Compose(
            UnitRagdoll ragdoll,
            UnitController controller,
            int delay)
        {
            _ragdoll = ragdoll;
            _controller = controller;
            _delay = delay;
        }

        public void SetPhysics(UnitPhysics physics)
        {
            _physics = physics;
        }

        public async void Start()
        {
            if(_isDead == false)
            {
                _isDead = true;
                _ragdoll.Enable();
                _ragdoll.Push(_physics, PUSH_FORCE);
                await Task.Delay(_delay);
                _controller.Dispose();
            }
        }

        public void Stop()
        {
            _isDead = false;
        }
    }
}