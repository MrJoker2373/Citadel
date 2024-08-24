namespace Citadel.Units
{
    using UnityEngine;
    using UnityEngine.AI;

    public class UnitAgent
    {
        private UnitController _controller;
        private UnitMachine _machine;
        private UnitRotation _rotation;
        private float _chaseRange;
        private float _stopRange;
        private UnitController _target;
        private UnitMachine _targetMachine;

        public void Compose(
            UnitController controller,
            UnitMachine state,
            UnitRotation rotation,
            float chaseRange,
            float stopRange)
        {
            _controller = controller;
            _machine = state;
            _rotation = rotation;
            _chaseRange = chaseRange;
            _stopRange = stopRange;
        }

        public void SetTarget(UnitController target)
        {
            _target = target;
            _targetMachine = target.GetUnitComponent<UnitMachine>();
        }

        public void Update()
        {
            if (_target == null)
                Idle();
            else if (_targetMachine.GetCurrentState() is UnitDeath)
                Idle();
            else
            {
                var distance = Vector3.Distance(_controller.GetPosition(), _target.GetPosition());
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
            var direction = _target.GetPosition() - _controller.GetPosition();
            _rotation.Rotate(direction);
            _machine.AttackState();
        }
        private bool FindPath(out Vector3 direction)
        {
            var layer = NavMesh.AllAreas;
            var path = new NavMeshPath();
            NavMesh.CalculatePath(_controller.GetPosition(), _target.GetPosition(), layer, path);
            bool hasPath = path.corners.Length != 0;
            direction = hasPath ? path.corners[1] - path.corners[0] : default;
            return hasPath;
        }
    }
}