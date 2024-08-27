namespace Citadel.Units
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class UnitContainer : MonoBehaviour
    {
        private List<object> _container;

        public void Compose()
        {
            _container = new();
        }

        public T Get<T>()
        {
            return _container.OfType<T>().Single();
        }

        public void Add(object item)
        {
            _container.Add(item);
        }

        public void Remove(object item)
        {
            _container.Remove(item);
        }
    }
}