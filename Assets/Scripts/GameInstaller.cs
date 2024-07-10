namespace Citadel.Unity
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Citadel.Unity.Input;
    using Citadel.Unity.Movement;
    public sealed class GameInstaller : MonoBehaviour
    {
        [Header("InputController")]
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private InputAction _movementDirection;
        [Header("OrientationController")]
        [SerializeField] private Transform _oreintationRoot;
        [Header("CameraController")]
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _up;
        [Header("PlayerController")]
        [SerializeField] private Rigidbody _player;
        [SerializeField] private float _playerSpeed;
        private InputController _input;
        private OrientationController _orientation;
        private void Awake()
        {
            InstallInput();
            InstallOrientation();
            InstallCamera();
            InstallPlayer();
        }
        private void InstallInput()
        {
            _input = new InputController(_mousePosition, _movementDirection);
            _input.Enable();
        }
        private void InstallOrientation()
        {
            _orientation = new OrientationController(_oreintationRoot);
        }
        private void InstallCamera()
        {
            var type = new TransformMovement(_camera);
            var movement = new MovementController(type, _cameraSpeed);
            var camera = new CameraController(_orientation, movement, _left, _right, _down, _up);
            _input.AddMousePositionObserver(camera);
        }
        private void InstallPlayer()
        {
            var type = new RigidbodyMovement(_player);
            var movement = new MovementController(type, _playerSpeed);
            var player = new PlayerController(_orientation, movement);
            _input.AddMovementDirectionObserver(player);
        }
    }
}