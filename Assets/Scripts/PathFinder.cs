namespace Citadel
{
    using UnityEngine;
    using UnityEngine.AI;

    public class PathFinder : MonoBehaviour
    {
        [SerializeField] private UnitMachine _source;
        [SerializeField] private UnitMachine _target;

        public void SetSource(UnitMachine source)
        {
            _source = source;
        }

        public void SetTarget(UnitMachine target)
        {
            _target = target;
        }

        public bool CanChase()
        {
            return _source != null && _target != null && _source.IsDead() == false && _target.IsDead() == false;
        }

        public float GetDistance()
        {
            return Vector3.Distance(_source.transform.position, _target.transform.position);
        }

        public bool FindPath(out Vector3 direction)
        {
            var layer = NavMesh.AllAreas;
            var path = new NavMeshPath();
            NavMesh.CalculatePath(_source.transform.position, _target.transform.position, layer, path);
            bool hasPath = path.corners.Length > 0;
            direction = hasPath ? (path.corners[1] - path.corners[0]).normalized : default;
            return hasPath;
        }
    }
}