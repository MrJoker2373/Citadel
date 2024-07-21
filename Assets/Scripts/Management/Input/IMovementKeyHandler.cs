namespace Citadel.Unity.Management.Input
{
    using UnityEngine;
    public interface IMovementKeyHandler
    {
        public void MovementKey(in Vector2 direction);
    }
}