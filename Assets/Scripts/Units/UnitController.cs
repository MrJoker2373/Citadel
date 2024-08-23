namespace Citadel.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public abstract class UnitController : MonoBehaviour
    {
        public Vector3 GetPosition() => transform.position;

        public abstract void Compose();

        public abstract void Dispose();

        public abstract T GetUnitComponent<T>();
    }
}