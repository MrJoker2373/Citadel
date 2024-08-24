namespace Citadel.Units
{
    public class UnitMovement : IDefaultState
    {
        private const string DEFAULT_KEY = "Default Movement";
        private const string CROUCH_KEY = "Crouch Movement";
        private UnitPhysics _physics;
        private UnitAnimation _animation;
        private float _defaultSpeed;
        private float _crouchSpeed;
        private bool _isMovement;
        private bool _isCrouch;

        public void Compose(
            UnitPhysics physics,
            UnitAnimation animation,
            float defaultSpeed, 
            float crawlSpeed)
        {
            _physics = physics;
            _animation = animation;
            _defaultSpeed = defaultSpeed;
            _crouchSpeed = crawlSpeed;
        }

        public void Start()
        {
            if (_isMovement == false)
            {
                _isMovement = true;
                _physics.Activate();
                if (_isCrouch == false)
                {
                    _animation.Play(DEFAULT_KEY);
                    _physics.SetSpeed(_defaultSpeed);
                }
                else
                {
                    _animation.Play(CROUCH_KEY);
                    _physics.SetSpeed(_crouchSpeed);
                }
            }
        }

        public void Stop()
        {
            _isMovement = false;
            _physics.Stop();
            _animation.Stop();
        }

        public void Default()
        {
            _isCrouch = false;
            if (_isMovement == true)
            {
                _physics.SetSpeed(_defaultSpeed);
                _animation.Play(DEFAULT_KEY);
            }
        }

        public void Crouch()
        {
            _isCrouch = true;
            if (_isMovement == true)
            {
                _physics.SetSpeed(_crouchSpeed);
                _animation.Play(CROUCH_KEY);
            }
        }
    }
}