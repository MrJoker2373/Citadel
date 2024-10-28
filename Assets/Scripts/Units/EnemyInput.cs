namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyMachine))]
    public class EnemyInput : MonoBehaviour
    {
        [SerializeField] private PlayerMachine _target;
        [SerializeField] private float _stopRange;
        [SerializeField] private float _chaseRange;
        private EnemyMachine _source;

        private void Awake()
        {
            _source = GetComponent<EnemyMachine>();
        }

        private void Update()
        {
            if (_target == null || _target.IsDead == true)
                _source.Idle();
            else
            {
                if (transform.FindPath(_target.transform.position, out var direction) == true)
                {
                    var distance = transform.GetDistance(_target.transform.position);
                    if (distance > _chaseRange)
                        _source.Idle();
                    else if (distance > _stopRange)
                    {
                        _source.Rotate(direction);
                        _source.Move();
                    }
                    else
                    {
                        _source.Rotate(direction);
                        StartCoroutine(_source.Attack());
                    }
                }
                else
                {
                    _source.Idle();
                }
            }
        }
    }
}