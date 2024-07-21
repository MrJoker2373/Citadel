namespace Citadel.Unity.Management.Initialization
{
    public interface IInitializationHandler
    {
        public void Install(in InitializationData data); 
    }
}