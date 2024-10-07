namespace Citadel
{
    using UnityEngine;

    [RequireComponent(typeof(PathFinder))]
    [RequireComponent(typeof(EnemyMachine))]
    public class EnemyInput : MonoBehaviour
    {
        [SerializeField] private float _stopRange;
        [SerializeField] private float _chaseRange;
        private PathFinder _path;
        private EnemyMachine _machine;

        private void Awake()
        {
            _path = GetComponent<PathFinder>();
            _machine = GetComponent<EnemyMachine>();
        }

        private void Update()
        {
            if (_path.CanChase() == false)
                _machine.Idle();
            else
            {
                if (_path.FindPath(out var direction) == true)
                {
                    var distance = _path.GetDistance();
                    if (distance > _chaseRange)
                        _machine.Idle();
                    else if (distance > _stopRange)
                    {
                        _machine.Rotate(direction);
                        _machine.Move();
                    }
                    else
                    {
                        _machine.Rotate(direction);
                        CoroutineLauncher.Launch(_machine.Attack());
                    }
                }
                else
                {
                    _machine.Idle();
                }
            }
        }
    }
}