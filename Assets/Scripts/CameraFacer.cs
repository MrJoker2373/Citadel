namespace Citadel
{
    using UnityEngine;

    public class CameraFacer : MonoBehaviour
    {
        private MainCamera _camera;

        private void Awake()
        {
            _camera = FindFirstObjectByType<MainCamera>();
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}