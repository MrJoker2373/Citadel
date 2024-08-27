namespace Citadel.Units
{
    using UnityEngine;

    public class UnitRagdoll
    {
        private UnitAnimation _animation;
        private Rigidbody _mainRigidbody;
        private Collider _mainCollider;
        private Rigidbody _rootRigidbody;
        private Collider _rootCollider;
        private Rigidbody[] _ragdollRigidbodies;
        private Collider[] _ragdollColliders;
        private bool _isRagdoll;

        public void Compose(
            UnitAnimation animation,
            Rigidbody mainRigidbody,
            Collider mainCollider,
            Rigidbody rootRigidbody,
            Collider rootCollider,
            Rigidbody[] ragdollRigidbodies,
            Collider[] ragdollColliders)
        {
            _animation = animation;
            _mainRigidbody = mainRigidbody;
            _mainCollider = mainCollider;
            _rootRigidbody = rootRigidbody;
            _rootCollider = rootCollider;
            _ragdollRigidbodies = ragdollRigidbodies;
            _ragdollColliders = ragdollColliders;
        }

        public void Enable()
        {
            _isRagdoll = true;
            _animation.Disable();
            _mainRigidbody.isKinematic = true;
            _mainCollider.enabled = false;
            _rootRigidbody.isKinematic = false;
            _rootCollider.enabled = true;
            foreach (var rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = false;
            foreach (var collider in _ragdollColliders)
                collider.enabled = true;
        }

        public void Disable()
        {
            _isRagdoll = false;
            _animation.Enable();
            _mainRigidbody.isKinematic = false;
            _mainCollider.enabled = true;
            _rootRigidbody.isKinematic = true;
            _rootCollider.enabled = false;
            foreach (var rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = true;
            foreach (var collider in _ragdollColliders)
                collider.enabled = false;
        }

        public void Push(Vector3 force)
        {
            if(_isRagdoll == true)
                _rootRigidbody.linearVelocity += force;
        }
    }
}