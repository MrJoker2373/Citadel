namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Ragdoll : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _mainRigidbody;
        private Collider _mainCollider;
        private RagdollComponent[] _components;

        public Rigidbody Rigidbody => _components[0].Rigidbody;

        public Collider Collider => _components[0].Collider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainRigidbody = GetComponent<Rigidbody>();
            _mainCollider = GetComponent<Collider>();
            _components = GetComponentsInChildren<RagdollComponent>();
            foreach (var component in _components)
                component.Initialize();
            Disable();
        }

        public void Enable()
        {
            _animator.enabled = false;
            _mainRigidbody.isKinematic = true;
            _mainCollider.enabled = false;
            foreach (var component in _components)
                component.Enable();
        }

        public void Disable()
        {
            _animator.enabled = true;
            _mainRigidbody.isKinematic = false;
            _mainCollider.enabled = true;
            foreach (var component in _components)
                component.Disable();
        }
    }
}