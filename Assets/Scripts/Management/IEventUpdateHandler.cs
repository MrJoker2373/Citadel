namespace Citadel.Unity.Management
{
    public interface IEventUpdateHandler
    {
        public void EventUpdate(in string key);
    }
}