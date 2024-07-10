namespace Citadel.Unity.Input
{
    using UnityEngine;
    public sealed class OrientationController
    {
        private readonly Transform _orientation;
        public OrientationController(Transform orientation)
        {
            _orientation = orientation;
        }
        public Vector3 GetDirection(bool isLeft, bool isRight, bool isDown, bool isUp)
        {
            var direction = Vector3.zero;
            if (isLeft == true)
                direction += -_orientation.right;
            else if (isRight == true)
                direction += _orientation.right;
            if (isDown == true)
                direction += -_orientation.forward;
            else if (isUp == true)
                direction += _orientation.forward;
            return direction;
        }
    }
}