namespace Citadel.Unity.Engine
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Entities;
    [DisallowMultipleComponent]
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputAction _cameraMovement;
        [SerializeField] private InputAction _playerMovement;
        [SerializeField] private InputAction _playerRoll;
        [SerializeField] private InputAction _playerPrimaryAttack;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [SerializeField] private OrientationController _orientation;
        [SerializeField] private CameraController _camera;
        [SerializeField] private PlayerController _player;
        private void Awake()
        {
            Enable();
        }
        private void Enable()
        {
            _cameraMovement.Enable();
            _playerMovement.Enable();
            _playerRoll.Enable();
            _playerPrimaryAttack.Enable();
            _cameraMovement.performed += CameraMovement;
            _playerMovement.performed += PlayerMovement;
            _playerRoll.performed += PlayerRoll;
            _playerPrimaryAttack.performed += PlayerPrimaryAttack;
        }
        private void Disable()
        {
            _cameraMovement.Disable();
            _playerMovement.Disable();
            _playerRoll.Disable();
            _playerPrimaryAttack.Disable();
            _cameraMovement.performed -= CameraMovement;
            _playerMovement.performed -= PlayerMovement;
            _playerRoll.performed -= PlayerRoll;
            _playerPrimaryAttack.performed -= PlayerPrimaryAttack;
        }
        private void CameraMovement(InputAction.CallbackContext context)
        {
            var position = context.ReadValue<Vector2>();
            var direction = Vector3.zero;
            if (_left.rect.Contains(_left.InverseTransformPoint(position)))
                direction += _orientation.GetLeft();
            else if (_right.rect.Contains(_right.InverseTransformPoint(position)))
                direction += _orientation.GetRight();
            if (_down.rect.Contains(_down.InverseTransformPoint(position)))
                direction += _orientation.GetBackward();
            else if (_up.rect.Contains(_up.InverseTransformPoint(position)))
                direction += _orientation.GetForward();
            if (direction == Vector3.zero)
                _camera.Stop();
            else
                _camera.Move(direction);
        }
        private void PlayerMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            if (direction == Vector2.zero)
                _player.Idle();
            else
            {
                var X = direction.x * _orientation.GetRight();
                var Z = direction.y * _orientation.GetForward();
                _player.Move(X + Z);
            }
        }
        private void PlayerRoll(InputAction.CallbackContext context)
        {
            _player.Roll();
        }
        private void PlayerPrimaryAttack(InputAction.CallbackContext context)
        {
            _player.PrimaryAttack();
        }
    }
}