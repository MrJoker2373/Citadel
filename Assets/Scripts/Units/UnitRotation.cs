namespace Citadel.Units
{
    using UnityEngine;

    public class UnitRotation
    {
        private const float ROTATION_THRESHOLD = 0.65f;
        private UnitMachine _machine;
        private UnitPhysics _physics;
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        public void Compose(UnitMachine machine, UnitPhysics physics, Rigidbody rigidbody)
        {
            _machine = machine;
            _physics = physics;
            _rigidbody = rigidbody;
        }

        public void Rotate(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;
            _direction = direction.normalized;
            _physics.SetDirection(_direction);
        }

        public void Update()
        {
            if(_machine.GetCurrentState() is IDefaultState)
            {
                var current = _rigidbody.rotation;
                var target = Quaternion.LookRotation(_direction);
                _rigidbody.rotation = Quaternion.Lerp(current, target, ROTATION_THRESHOLD);
            }
        }
    }
}