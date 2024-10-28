namespace Citadel.Units
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
        private PlayerMachine _machine;
        private Vector3 _forwardAxis;
        private Vector3 _rightAxis;

        private void Awake()
        {
            _machine = GetComponent<PlayerMachine>();
            _forwardAxis = transform.forward;
            _rightAxis = transform.right;
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
            var direction = input.x * _rightAxis + input.y * _forwardAxis;
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
            StartCoroutine(_machine.Roll());
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            StartCoroutine(_machine.Attack());
        }

        private void OnCollect(InputAction.CallbackContext context)
        {
            _machine.Collect();
        }
    }
}