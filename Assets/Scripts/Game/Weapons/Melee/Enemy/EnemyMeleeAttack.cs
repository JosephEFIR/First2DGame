using Audio;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enemies;
using Scripts.Enums;
using Units.Enemies;
using UnityEngine;

namespace Scripts.Weapons.Melee
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private MeleePoint _meleePoint;

        private LocalAudioService _audioService;
        private CustomAnimator _animator;
        private EnemyAI _enemyAI;
        private UnitConfig _config;

        private int _damage;
        
        private void Awake()
        {
            _audioService = GetComponent<LocalAudioService>();
            EnemyController enemyController = GetComponent<EnemyController>();
            _animator = GetComponent<CustomAnimator>();
            _enemyAI = GetComponent<EnemyAI>();
            _config = enemyController.Config;
        }

        private void Start()
        {
            _damage = _config.UnitStats[EUnitStat.Damage];
        }

        private void Update()
        {
            if (_meleePoint.CanAttack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _enemyAI.Stay(true);
            _audioService.Play(EClipType.Swing);
            _animator.SetTrigger(EAnimationType.Attack); 
        }
        
        private void OnAttack()
        {
            if (_meleePoint.CanAttack & _meleePoint.EnemyHealth != null)
            {
                _audioService.Play(EClipType.Punch);
                _meleePoint.EnemyHealth.GetDamage(_damage);
            }
            _enemyAI.Stay(false);
            _animator.SetTrigger(EAnimationType.Idle);
        }
    } 
}
