namespace Citadel.Units
{
    using UnityEngine;

    public abstract class UnitController : MonoBehaviour
    {
        public abstract void Compose();

        public abstract void Dispose();
    }
}