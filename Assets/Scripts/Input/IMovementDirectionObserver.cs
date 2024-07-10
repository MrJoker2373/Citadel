namespace Citadel.Unity.Input
{
    using UnityEngine;
    public interface IMovementDirectionObserver
    {
        public void OnMovementDirectionChanged(Vector2 direction);
    }
}