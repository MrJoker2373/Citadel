namespace Citadel.Units
{
    using UnityEngine;
    using UnityEngine.AI;

    public class UnitAgent
    {
        private UnitMachine _machine;
        private UnitPhysics _physics;
        private UnitRotation _rotation;
        private float _chaseRange;
        private float _stopRange;
        private UnitContainer _target;
        private UnitMachine _targetMachine;
        private UnitPhysics _targetPhysics;

        public void Compose(
            UnitMachine machine,
            UnitPhysics physics,
            UnitRotation rotation,
            float chaseRange,
            float stopRange)
        {
            _machine = machine;
            _physics = physics;
            _rotation = rotation;
            _chaseRange = chaseRange;
            _stopRange = stopRange;
        }

        public void SetTarget(UnitContainer target)
        {
            _target = target;
            _targetMachine = target.Get<UnitMachine>();
            _targetPhysics = target.Get<UnitPhysics>();
        }

        public void Update()
        {
            if (_target == null)
                Idle();
            else if (_targetMachine.GetCurrentState() is UnitDeath)
                Idle();
            else
            {
                var distance = Vector3.Distance(_physics.GetPosition(), _targetPhysics.GetPosition());
                if (distance > _chaseRange)
                    Idle();
                else if (distance > _stopRange)
                    Chase();
                else
                    Attack();
            }
        }
        private void Idle()
        {
            _machine.IdleState();
        }
        private void Chase()
        {
            if (FindPath(out var direction) == true)
            {
                _rotation.Rotate(direction);
                _machine.MovementState();
            }
            else
            {
                _machine.IdleState();
            }
        }
        private void Attack()
        {
            var direction = _targetPhysics.GetPosition() - _physics.GetPosition();
            _rotation.Rotate(direction);
            _machine.AttackState();
        }
        private bool FindPath(out Vector3 direction)
        {
            var layer = NavMesh.AllAreas;
            var path = new NavMeshPath();
            NavMesh.CalculatePath(_physics.GetPosition(), _targetPhysics.GetPosition(), layer, path);
            bool hasPath = path.corners.Length != 0;
            direction = hasPath ? path.corners[1] - path.corners[0] : default;
            return hasPath;
        }
    }
}