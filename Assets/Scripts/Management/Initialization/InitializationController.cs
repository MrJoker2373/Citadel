namespace Citadel.Unity.Management.Initialization
{
    using UnityEngine;
    public sealed class InitializationController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _handlers;
        private InitializationData _data;
        private void Awake()
        {
            _data = new();
            foreach (var handler in _handlers)
            {
                if (handler is IInitializationHandler item)
                    item.Install(_data);
            }
        }
        private void Update()
        {
            foreach (var handler in _handlers)
            {
                if (handler is IRenderUpdateHandler item)
                    item.RenderUpdate(Time.deltaTime);
            }
        }
        private void LateUpdate()
        {
            foreach (var handler in _handlers)
            {
                if (handler is ILastUpdateHandler item)
                    item.LastUpdate(Time.deltaTime);
            }
        }
        private void FixedUpdate()
        {
            foreach (var handler in _handlers)
            {
                if (handler is IPhysicsUpdateHandler item)
                    item.PhysicsUpdate(Time.fixedDeltaTime);
            }
        }
    }
}