namespace Citadel.Unity.Entities
{
    public interface IUnitContainer
    {
        public T GetUnitComponent<T>() where T : class;
    }
}