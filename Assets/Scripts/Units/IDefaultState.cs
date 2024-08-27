namespace Citadel.Units
{
    public interface IDefaultState : IUnitState
    {
        public void Start();

        public void Stop();

        public void Default();

        public void Crouch();
    }
}