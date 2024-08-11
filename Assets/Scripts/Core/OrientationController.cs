namespace Citadel.Unity.Core
{
    using UnityEngine;
    [DisallowMultipleComponent()]
    public class OrientationController : MonoBehaviour
    {
        public Vector3 GetLeft()
        {
            return -transform.right;
        }
        public Vector3 GetRight()
        {
            return transform.right;
        }
        public Vector3 GetDown()
        {
            return -transform.up;
        }
        public Vector3 GetUp()
        {
            return transform.up;
        }
        public Vector3 GetBackward()
        {
            return -transform.forward;
        }
        public Vector3 GetForward()
        {
            return transform.forward;
        }
    }
}