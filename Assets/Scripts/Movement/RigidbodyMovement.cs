namespace Citadel.Unity.Movement
{
    using UnityEngine;
    public sealed class RigidbodyMovement : IMovementType
    {
        private readonly Rigidbody _rigidbody;
        public RigidbodyMovement(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }
        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity += velocity;
        }
    }
}