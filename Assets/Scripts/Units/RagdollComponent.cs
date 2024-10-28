namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class RagdollComponent : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Collider _collider;

        public Rigidbody Rigidbody => _rigidbody;

        public Collider Collider => _collider;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Enable()
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }

        public void Disable()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
    }
}