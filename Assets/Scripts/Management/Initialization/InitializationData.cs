namespace Citadel.Unity.Management.Initialization
{
    using System.Collections.Generic;
    using System.Linq;
    public sealed class InitializationData
    {
        private readonly List<object> _items;
        public InitializationData()
        {
            _items = new();
        }
        public T Get<T>()
        {
            return _items.OfType<T>().Single();
        }
        public void Add(object item)
        {
            _items.Add(item);
        }
        public void Remove(object item)
        {
            _items.Remove(item);
        }
    }
}