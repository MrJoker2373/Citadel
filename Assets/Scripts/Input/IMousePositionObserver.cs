namespace Citadel.Unity.Input
{
    using UnityEngine;
    public interface IMousePositionObserver
    {
        public void OnMousePositionChanged(Vector2 position);
    }
}