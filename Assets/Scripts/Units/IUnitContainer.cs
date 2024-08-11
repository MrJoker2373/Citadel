namespace Citadel.Unity.Units
{
    public interface IUnitContainer
    {
        public T GetUnitComponent<T>() where T : class;
    }
}