using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Weapons.Melee
{
    public class MeleePoint : MonoBehaviour
    {
        public bool CanAttack { get; private set; }
        
        private IHealthSystem _unitHealth;
        public IHealthSystem EnemyHealth;

        private void Awake()
        {
            _unitHealth = GetComponentInParent<IHealthSystem>();
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out IHealthSystem enemyHealth))
            {
                EnemyHealth = enemyHealth;
                if (EnemyHealth!= _unitHealth)
                {
                    CanAttack = true;
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out IHealthSystem enemyHealth))
            {
                EnemyHealth = enemyHealth;
                if (EnemyHealth!= _unitHealth)
                {
                    CanAttack = false;
                }
            }
        }
    }
}