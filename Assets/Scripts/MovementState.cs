namespace Citadel
{
    using UnityEngine;

    [RequireComponent(typeof(AnimationPlayer))]
    [RequireComponent(typeof(Rigidbody))]
    public class MovementState : MonoBehaviour
    {
        private const string STAND_IDLE_ANIMATION = "Stand Idle";
        private const string STAND_MOVE_ANIMATION = "Stand Move";
        private const string CROUCH_IDLE_ANIMATION = "Crouch Idle";
        private const string CROUCH_MOVE_ANIMATION = "Crouch Move";
        [SerializeField] private float _standSpeed;
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private float _rotationSpeed;
        private AnimationPlayer _animation;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _currentSpeed;
        private bool _isActive;
        private bool _isMove;
        private bool _isCrouch;

        private void Awake()
        {
            _animation = GetComponent<AnimationPlayer>();
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
                    _rigidbody.linearVelocity += _currentSpeed * _direction;
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
                    _animation.Play(STAND_IDLE_ANIMATION);
                else
                {
                    _currentSpeed = _standSpeed;
                    _animation.Play(STAND_MOVE_ANIMATION);
                }
            }
            else
            {
                if (_isMove == false)
                    _animation.Play(CROUCH_IDLE_ANIMATION);
                else
                {
                    _animation.Play(CROUCH_MOVE_ANIMATION);
                    _currentSpeed = _crouchSpeed;
                }
            }
        }
    }
}