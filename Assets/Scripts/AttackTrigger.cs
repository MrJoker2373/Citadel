﻿namespace Citadel
{
    using UnityEngine;

    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] private AttackState _attack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var hit))
                _attack.SetHit(hit);
        }
    }
}