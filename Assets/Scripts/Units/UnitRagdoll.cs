namespace Citadel.Units
{
    using UnityEngine;

    public class UnitRagdoll
    {
        private UnitAnimation _animation;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Rigidbody[] _ragdollRigidbodies;
        private Collider[] _ragdollColliders;

        public void Compose(
            UnitAnimation animation,
            Rigidbody rigidbody,
            Collider collider,
            Rigidbody[] ragdollRigidbodies,
            Collider[] ragdollColliders)
        {
            _animation = animation;
            _rigidbody = rigidbody;
            _collider = collider;
            _ragdollRigidbodies = ragdollRigidbodies;
            _ragdollColliders = ragdollColliders;
        }

        public void Enable()
        {
            _animation.Disable();
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            foreach (var rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = false;
            foreach (var collider in _ragdollColliders)
                collider.enabled = true;
        }

        public void Disable()
        {
            _animation.Enable();
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
            foreach (var rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = true;
            foreach (var collider in _ragdollColliders)
                collider.enabled = false;
        }
    }
}