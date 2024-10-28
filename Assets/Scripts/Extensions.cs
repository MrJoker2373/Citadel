namespace Citadel
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;

    public static class Extensions
    {
        public static IEnumerator PlayAsync(this Animator animator, string animation, float fade)
        {
            animator.CrossFadeInFixedTime(animation, fade);
            yield return new WaitForSeconds(fade + 0.05f);
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
                yield return null;
        }

        public static bool FindPath(this Transform source, Vector3 target, out Vector3 result)
        {
            var path = new NavMeshPath();
            NavMesh.CalculatePath(source.position, target, NavMesh.AllAreas, path);
            bool hasPath = path.corners.Length > 0;
            result = hasPath ? (path.corners[1] - path.corners[0]).normalized : default;
            return hasPath;
        }

        public static float GetDistance(this Transform source, Vector3 target)
        {
            return Vector3.Distance(source.transform.position, target);
        }
    }
}