namespace Citadel.Unity.Movement
{
    using UnityEngine;
    public interface IMovementType
    {
        public void Move(Vector3 velocity);
    }
}