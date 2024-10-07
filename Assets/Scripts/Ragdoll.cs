namespace Citadel
{
    using UnityEngine;

    [RequireComponent(typeof(AnimationPlayer))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private RagdollComponent _root;
        [SerializeField] private RagdollComponent[] _components;
        private AnimationPlayer _animation;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private bool _isRagdoll;

        private void Awake()
        {
            _animation = GetComponent<AnimationPlayer>();
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Disable();
        }

        public void Enable()
        {
            _isRagdoll = true;
            _animation.Disable();
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            _root.Enable();
            foreach (var component in _components)
                component.Enable();
        }

        public void Disable()
        {
            _isRagdoll = false;
            _animation.Enable();
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            _root.Disable();
            foreach (var component in _components)
                component.Disable();
        }

        public void Push(Vector3 force)
        {
            if (_isRagdoll == true)
                _root.Push(force);
        }
    }
}