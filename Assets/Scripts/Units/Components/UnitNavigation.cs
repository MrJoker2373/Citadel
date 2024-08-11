namespace Citadel.Unity.Units.Components
{
    using UnityEngine;
    using UnityEngine.AI;
    public class UnitNavigation
    {
        private readonly UnitMovement _movement;
        private readonly UnitAttack _attack;
        private readonly Transform _target;
        private readonly float _chaseRange;
        private readonly float _stopRange;
        public UnitNavigation(
            UnitMovement movement,
            UnitAttack attack,
            Transform target,
            float chaseRange,
            float stopRange)
        {
            _movement = movement;
            _attack = attack;
            _target = target;
            _chaseRange = chaseRange;
            _stopRange = stopRange;
        }
        public void Update()
        {
            if (_target == null)
                _movement.IdleState();
            else
            {
                var distance = Vector3.Distance(_movement.GetPosition(), _target.position);
                if (distance <= _stopRange)
                    Attack();
                else if (distance > _chaseRange)
                    _movement.IdleState();
                else
                {
                    var path = new NavMeshPath();
                    NavMesh.CalculatePath(_movement.GetPosition(), _target.position, NavMesh.AllAreas, path);
                    if (path.corners.Length == 0)
                        _movement.IdleState();
                    else
                    {
                        var direction = path.corners[1] - path.corners[0];
                        _movement.SetDirection(direction.normalized);
                        _movement.MoveState();
                    }
                }
            }
        }
        private async void Attack()
        {
            if (_movement.IsEnabled() == true)
            {
                _movement.ExitState();
                await _attack.ProcessState();
                _movement.EnterState();
            }
        }
    }
}