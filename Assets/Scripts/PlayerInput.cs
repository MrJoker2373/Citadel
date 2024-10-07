namespace Citadel
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    [RequireComponent(typeof(PlayerMachine))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputAction _moveInput;
        [SerializeField] private InputAction _couchInput;
        [SerializeField] private InputAction _rollInput;
        [SerializeField] private InputAction _attackInput;
        [SerializeField] private InputAction _collectInput;
        private Vector3 _forward;
        private Vector3 _right;
        private PlayerMachine _machine;

        private void Awake()
        {
            _machine = GetComponent<PlayerMachine>();
        }

        private void Start()
        {
            _forward = transform.forward;
            _right = transform.right;
            Enable();
        }

        public void Enable()
        {
            _moveInput.Enable();
            _couchInput.Enable();
            _rollInput.Enable();
            _attackInput.Enable();
            _collectInput.Enable();
            _moveInput.performed += OnMove;
            _couchInput.performed += OnCrouch;
            _rollInput.performed += OnRoll;
            _attackInput.performed += OnAttack;
            _collectInput.performed += OnCollect;
        }

        public void Disable()
        {
            _moveInput.Disable();
            _couchInput.Disable();
            _rollInput.Disable();
            _attackInput.Disable();
            _collectInput.Disable();
            _moveInput.performed -= OnMove;
            _couchInput.performed -= OnCrouch;
            _rollInput.performed -= OnRoll;
            _attackInput.performed -= OnAttack;
            _collectInput.performed -= OnCollect;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            var direction = input.x * _right + input.y * _forward;
            _machine.Rotate(direction);
            if (direction == Vector3.zero)
                _machine.Idle();
            else
                _machine.Move();
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<float>();
            if (input == 0)
                _machine.Stand();
            else
                _machine.Crouch();
        }

        private void OnRoll(InputAction.CallbackContext context)
        {
            CoroutineLauncher.Launch(_machine.Roll());
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            CoroutineLauncher.Launch(_machine.Attack());
        }

        private void OnCollect(InputAction.CallbackContext context)
        {
            _machine.Collect();
        }
    }
}