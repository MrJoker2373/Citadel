namespace Citadel.Unity.Entities
{
    using UnityEngine;
    public class CoinController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player) == true)
            {
                player.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}