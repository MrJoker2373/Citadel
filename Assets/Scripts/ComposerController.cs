namespace Citadel
{
    using UnityEngine;
    [DisallowMultipleComponent]
    public class ComposerController : MonoBehaviour
    {
        private void Awake()
        {
            var camera = FindObjectOfType<CameraController>();
            camera.Compose();
            var player = FindObjectOfType<PlayerController>();
            player.Compose();
            var enemy = FindObjectOfType<EnemyController>();
            enemy.Compose();
        }
    }
}