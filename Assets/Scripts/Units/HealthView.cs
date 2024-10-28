namespace Citadel.Units
{
    using System.Collections;
    using UnityEngine;

    public abstract class HealthView : MonoBehaviour
    {
        public abstract IEnumerator Refresh(float health, float capacity);
    }
}