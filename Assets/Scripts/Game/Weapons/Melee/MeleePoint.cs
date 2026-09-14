using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Weapons.Melee
{
    public class MeleePoint : MonoBehaviour
    {
        public bool CanAttack { get; private set; }
        public IHealthSystem HealthEntered;
        

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out IHealthSystem health))
            {
                HealthEntered = health;
                CanAttack = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out IHealthSystem health))
            {
                HealthEntered = null;
                CanAttack = false;
            }
        }
    }
}