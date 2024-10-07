namespace Citadel
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class RagdollComponent : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Enable()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public void Disable()
        {
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void Push(Vector3 force)
        {
            _rigidbody.linearVelocity += force;
        }
    }
}