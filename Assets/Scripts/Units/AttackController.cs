namespace Citadel.Units
{
    using UnityEngine;

    public class AttackController : MonoBehaviour
    {
        [SerializeField] private UnitController _controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<UnitController>(out var controller))
            {
                var attack = _controller.GetUnitComponent<UnitAttack>();
                attack.SetHit(controller.GetUnitComponent<UnitHealth>());
            }
        }
    }
}