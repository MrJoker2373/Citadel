namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class MovementState : MonoBehaviour
    {
        private const float CROSS_FADE = 0.125f;
        private const string STAND_IDLE_ANIMATION = "Stand Idle";
        private const string STAND_MOVE_ANIMATION = "Stand Move";
        private const string CROUCH_IDLE_ANIMATION = "Crouch Idle";
        private const string CROUCH_MOVE_ANIMATION = "Crouch Move";
        [SerializeField] private float _standSpeed;
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private float _rotationSpeed;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _currentSpeed;
        private bool _isActive;
        private bool _isMove;
        private bool _isCrouch;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _direction = transform.forward;
        }

        private void FixedUpdate()
        {
            if (_isActive == true)
            {
                var current = _rigidbody.rotation;
                var target = Quaternion.LookRotation(_direction);
                _rigidbody.rotation = Quaternion.Lerp(current, target, _rotationSpeed);

                if (_isMove == true)
                {
                    _rigidbody.velocity += _currentSpeed * _direction;
                }
            }
        }

        public void Rotate(Vector3 direction)
        {
            if (direction != Vector3.zero)
                _direction = direction.normalized;
        }

        public void Enter()
        {
            _isActive = true;
            Refresh();
        }

        public void Exit()
        {
            _isActive = false;
        }

        public void Idle()
        {
            if (_isMove == false)
                return;
            _isMove = false;
            if (_isActive == true)
                Refresh();
        }

        public void Move()
        {
            if (_isMove == true)
                return;
            _isMove = true;
            if (_isActive == true)
                Refresh();
        }

        public void Stand()
        {
            _isCrouch = false;
            if (_isActive == true)
                Refresh();
        }

        public void Crouch()
        {
            _isCrouch = true;
            if (_isActive == true)
                Refresh();
        }

        private void Refresh()
        {
            if (_isCrouch == false)
            {
                if (_isMove == false)
                    _animator.CrossFadeInFixedTime(STAND_IDLE_ANIMATION, CROSS_FADE);
                else
                {
                    _currentSpeed = _standSpeed;
                    _animator.CrossFadeInFixedTime(STAND_MOVE_ANIMATION, CROSS_FADE);
                }
            }
            else
            {
                if (_isMove == false)
                    _animator.CrossFadeInFixedTime(CROUCH_IDLE_ANIMATION, CROSS_FADE);
                else
                {
                    _animator.CrossFadeInFixedTime(CROUCH_MOVE_ANIMATION, CROSS_FADE);
                    _currentSpeed = _crouchSpeed;
                }
            }
        }
    }
}