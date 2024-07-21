namespace Citadel.Unity.Management
{
    using UnityEngine;
    public sealed class OrientationController
    {
        private readonly Transform _orientation;
        public OrientationController(Transform orientation)
        {
            _orientation = orientation;
        }
        public Vector3 GetXAxis()
        {
            return _orientation.right;
        }
        public Vector3 GetYAxis()
        {
            return _orientation.up;
        }
        public Vector3 GetZAxis()
        {
            return _orientation.forward;
        }
    }
}