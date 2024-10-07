namespace Citadel
{
    using UnityEngine;

    public abstract class UnitMachine : MonoBehaviour
    {
        public abstract bool IsDead();

        public abstract void Die(Vector3 knockback);
    }
}