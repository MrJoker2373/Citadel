namespace Citadel.Unity
{
    using UnityEngine;
    using Citadel.Unity.Input;
    using Citadel.Unity.Movement;
    public sealed class PlayerController : IMovementDirectionObserver
    {
        private readonly OrientationController _orientation;
        private readonly MovementController _movement;
        public PlayerController(OrientationController orientation, MovementController movement)
        {
            _orientation = orientation;
            _movement = movement;
        }
        public void OnMovementDirectionChanged(Vector2 input)
        {
            var isLeft = input.x < 0;
            var isRight = input.x > 0;
            var isDown = input.y < 0;
            var isUp = input.y > 0;
            if (isLeft == false && isRight == false && isDown == false && isUp == false)
                _movement.Stop();
            else
            {
                var direction = _orientation.GetDirection(isLeft, isRight, isDown, isUp);
                _movement.SetDirection(direction.normalized);
                if (_movement.IsMoving() == false)
                    _movement.Move();
            }
        }
    }
}