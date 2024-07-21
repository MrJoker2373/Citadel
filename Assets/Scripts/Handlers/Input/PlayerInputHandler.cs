namespace Citadel.Unity.Handlers.Input
{
    using UnityEngine;
    using Citadel.Unity.Management;
    using Citadel.Unity.Management.Input;
    public sealed class PlayerInputHandler : IMovementKeyHandler, IRollKeyHandler
    {
        private readonly PlayerController _player;
        private readonly OrientationController _orientation;
        public PlayerInputHandler(PlayerController player, OrientationController orientation)
        {
            _player = player;
            _orientation = orientation;
        }
        public void MovementKey(in Vector2 direction)
        {
            if (direction == Vector2.zero)
                _player.Idle();
            else
            {
                var X = direction.x * _orientation.GetXAxis();
                var Z = direction.y * _orientation.GetZAxis();
                _player.Rotate(X + Z);
                _player.Move();
            }
        }
        public void RollKey()
        {
            _player.Roll();
        }
    }
}