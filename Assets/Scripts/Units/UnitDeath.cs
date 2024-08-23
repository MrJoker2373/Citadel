namespace Citadel.Units
{
    using System.Threading.Tasks;

    public class UnitDeath : IDeathState
    {
        private UnitRagdoll _ragdoll;
        private UnitController _controller;
        private int _delay;
        private bool _isActive;

        public bool IsActive()
        {
            return _isActive;
        }

        public void Compose(
            UnitRagdoll ragdoll,
            UnitController controller,
            int delay)
        {
            _ragdoll = ragdoll;
            _controller = controller;
            _delay = delay;
        }

        public async void Start()
        {
            _isActive = true;
            _ragdoll.Enable();
            await Task.Delay(_delay);
            _controller.Dispose();
        }
    }
}