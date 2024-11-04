using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.Player;
using UnityEngine;
using Zenject;

namespace  Scripts.Weapons.Melee
{
    public class MeleeAttackController : MonoBehaviour
    {
        [SerializeField] private MeleePoint _meleePoint;
        
        private CustomAnimator _animator;
        private LocalAudioService _audioService;
        private UnitConfig _config;

        private CancellationTokenSource _token;
        private const int _tick = 350;
        
        private int _damage;
        
        private void Awake()
        {
            _audioService = GetComponent<LocalAudioService>();
            _animator = GetComponent<CustomAnimator>();
            PlayerController _player = GetComponent<PlayerController>();
            
            _config = _player.Config;
        }

        private void Start()
        {
            _damage = _config.UnitStats[EUnitStat.Damage];
        }

        private void Update()
        {
            if (Input.GetKeyDown((KeyCode.J))) //TODO refactor ball mode & attack mode , State machine?
            {
                if (_token is null)
                {
                    _token = new CancellationTokenSource();
                    Attack().Forget();
                }
            }
        }
        private async UniTaskVoid Attack()
        {
            _audioService.Play(EClipType.Swing);
            
            _animator.SetTrigger(EAnimationType.Attack); 
            await UniTask.Delay(_tick, cancellationToken: _token.Token);
            StopTick();
        }

        private void StopTick()
        {
            _token?.Cancel();
            _token?.Dispose();
            _token = null;
        }

        private void OnDestroy()
        {
            StopTick();
        }

        private void OnAttack()
        {
            if (_meleePoint.CanAttack & _meleePoint.EnemyHealth != null)
            {
                _meleePoint.EnemyHealth.GetDamage(_damage);
                _audioService.Play(EClipType.Punch);
                _audioService.Play(EClipType.Punch);
            }
            _animator.SetTrigger(EAnimationType.Idle);
        }
    }
}