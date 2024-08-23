namespace Citadel.Units
{
    using UnityEngine;

    public interface IOrientationController
    {
        public Vector3 GetLeft();

        public Vector3 GetRight();

        public Vector3 GetDown();

        public Vector3 GetUp();

        public Vector3 GetBackward();

        public Vector3 GetForward();
    }
}