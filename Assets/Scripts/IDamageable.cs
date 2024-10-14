namespace Citadel
{
    using UnityEngine;

    public interface IDamageable
    {
        public void Damage(int health, Vector3 knockback);
    }
}