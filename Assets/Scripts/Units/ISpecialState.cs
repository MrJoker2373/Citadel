namespace Citadel.Units
{
    using System.Threading.Tasks;

    public interface ISpecialState : IUnitState
    {
        public Task Run();
    }
}