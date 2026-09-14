using Assets.Scripts.Interfaces;
using Scripts.Weapons.Melee;
using UnityEngine;

namespace Game.Animators
{
    public class UnitAnimationEventReceiver : MonoBehaviour
    {
        private IAttack _attack;
        private IHealthSystem _healthSystem;
        
        public void Init(IAttack attack, IHealthSystem healthSystem)
        {
            _attack = attack;
            _healthSystem = healthSystem;
        }

        public void OnAttack()
        {
            _attack?.OnAttack();
        }

        public void OnDead()
        {
            _healthSystem?.OnDeath();
        }
    }
}