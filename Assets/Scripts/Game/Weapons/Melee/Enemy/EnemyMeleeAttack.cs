using Game.Units.Enemies;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enemies;
using Scripts.Enums;
using Scripts.Health;
using Scripts.Player;

namespace Scripts.Weapons.Melee
{
    public class EnemyMeleeAttack : IAttack
    {
        private MeleePoint _meleePoint;
        
        private CustomAnimator _animator;
        private EnemyAIController _enemyAIController;
        private UnitConfig _config;

        private int _damage;

        public void Init(EnemyModel model)
        {
            _config = model.Config;
            _meleePoint = model.MeleePoint;
            _enemyAIController = model.AI_Controller;
            _animator = model.Animator;
            _damage = _config.UnitStats[EUnitStat.Damage];
        }

        public void Run()
        {
            if (_meleePoint.CanAttack && _meleePoint.HealthEntered is PlayerHealth)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _enemyAIController.Stay(true);
            _animator.SetTrigger(EAnimationType.Attack); 
        }
        
        public void OnAttack()
        {
            if (_meleePoint.CanAttack & _meleePoint.HealthEntered != null)
            {
                _meleePoint.HealthEntered.GetDamage(_damage);
            }
            _enemyAIController.Stay(false);
            _animator.SetTrigger(EAnimationType.Idle);
        }
    } 
}
