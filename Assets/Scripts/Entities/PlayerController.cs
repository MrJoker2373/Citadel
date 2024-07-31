namespace Citadel.Unity.Entities
{
    using UnityEngine;
    using TMPro;
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        private const string IDLE_KEY = "Idle";
        private const string MOVE_KEY = "Move";
        private const string ROLL_KEY = "Roll";
        private const string PRIMARY_ATTACK_KEY = "PrimaryAttack";
        private const float ROTATION_SPEED = 0.4f;
        [SerializeField] private TextMeshProUGUI _coinLabel;
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private Vector3 _direction;
        private Vector3 _lastDirection;
        private int _coins;
        private bool _isMoving;
        private bool _isRolling;
        private bool _isAttacking;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            Rotate(_direction);
        }
        public void Idle()
        {
            _isMoving = false;
            if (_isRolling == false && _isAttacking == false)
                _animator.Play(IDLE_KEY);
        }
        public void Move(Vector3 direction)
        {
            _isMoving = true;
            _lastDirection = direction.normalized;
            if (_isRolling == false && _isAttacking == false)
            {
                _direction = _lastDirection;
                _animator.Play(MOVE_KEY);
            }
        }
        public void Roll()
        {
            if (_isAttacking == false)
            {
                _isRolling = true;
                _animator.Play(ROLL_KEY);
            }
        }
        public void PrimaryAttack()
        {
            if (_isRolling == false && _isAttacking == false)
            {
                _isAttacking = true;
                _animator.Play(PRIMARY_ATTACK_KEY);
            }
        }
        public void RollStop()
        {
            _isRolling = false;
            if (_isMoving == false)
                Idle();
            else
                Move(_lastDirection);
        }
        public void AttackStop()
        {
            _isAttacking = false;
            if (_isMoving == false)
                Idle();
            else
                Move(_lastDirection);
        }
        public void Rotate(Vector3 direction)
        {
            var rotation = Quaternion.LookRotation(direction);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, rotation, ROTATION_SPEED);
        }
        public void AddCoin()
        {
            _coins++;
            _coinLabel.text = _coins.ToString();
        }
    }
}