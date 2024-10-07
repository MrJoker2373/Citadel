namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class CoroutineLauncher : MonoBehaviour
    {
        private static MonoBehaviour _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static Coroutine Launch(IEnumerator coroutine)
        {
            return _instance.StartCoroutine(coroutine);
        }

        public static void Stop(Coroutine coroutine)
        {
            if (coroutine != null)
                _instance.StopCoroutine(coroutine);
        }
    }
}