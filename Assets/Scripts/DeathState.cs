namespace Citadel
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Ragdoll))]
    public class DeathState : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private float _delay;
        private Ragdoll _ragdoll;
        private bool _isDead;

        public bool IsDead => _isDead;

        private void Awake()
        {
            _ragdoll = GetComponent<Ragdoll>();
        }

        public IEnumerator Die(Vector3 force)
        {
            _isDead = true;
            _ragdoll.Enable();
            _ragdoll.Push(force);
            yield return new WaitForSeconds(_delay);
            Destroy(_root);
        }
    }
}