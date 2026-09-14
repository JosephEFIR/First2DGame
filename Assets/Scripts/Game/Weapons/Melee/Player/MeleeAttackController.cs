using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.Player;
using Scripts.Units;
using UnityEngine;

namespace Scripts.Weapons.Melee
{
    public class MeleeAttackController : IAttack
    {
        private MeleePoint _meleePoint;
        private StateMachine _stateMachine;
        
        private CustomAnimator _animator;
        private UnitConfig _config;

        private CancellationTokenSource _token;
        private const int _tick = 350;
        
        private int _damage;

        public void Init(UnitModel model)
        {
            _animator = model.Animator;
            _config = model.Config;
            _meleePoint = model.MeleePoint;
            _stateMachine = model.StateMachine;
            _damage = _config.UnitStats[EUnitStat.Damage];
        }

        public void Run()
        {
            if (Input.GetKeyDown((KeyCode.J)) & !_stateMachine.States[EState.BallMode])
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

        public void OnDestroy()
        {
            StopTick();
        }

        public void OnAttack()
        {
            if (_meleePoint.CanAttack & _meleePoint.HealthEntered != null)
            {
                _meleePoint.HealthEntered.GetDamage(_damage);
            }
            _animator.SetTrigger(EAnimationType.Idle);
        }
    }
}