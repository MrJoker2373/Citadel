namespace Citadel.Unity.Movement
{
    using UnityEngine;
    public sealed class TransformMovement : IMovementType
    {
        private readonly Transform _transform;
        public TransformMovement(Transform transform)
        {
            _transform = transform;
        }
        public void Move(Vector3 velocity)
        {
            _transform.position += velocity;
        }
    }
}