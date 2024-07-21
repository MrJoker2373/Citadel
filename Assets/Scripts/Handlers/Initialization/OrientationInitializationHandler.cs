namespace Citadel.Unity.Handlers.Initialization
{
    using UnityEngine;
    using Citadel.Unity.Management;
    using Citadel.Unity.Management.Initialization;
    public sealed class OrientationInitializationHandler : MonoBehaviour, IInitializationHandler
    {
        [SerializeField] private Transform _transform;
        private OrientationController _controller;
        public void Install(in InitializationData data)
        {
            _controller = new OrientationController(_transform);
            data.Add(_controller);
        }
    }
}