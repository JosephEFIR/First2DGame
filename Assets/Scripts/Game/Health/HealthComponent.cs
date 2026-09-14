using System;
using Assets.Scripts.Interfaces;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.Units;
using UniRx;
using Units;
using UnityEngine;

namespace Health
{
    public abstract class HealthComponent : MonoBehaviour,IHealthSystem
    {
        protected UnitConfig _config;
        protected CapsuleCollider2D _collider;
        protected UnitView _view;

        protected CustomAnimator _animator;

        public readonly ReactiveProperty<int> CurrentHealth = new();
        public readonly ReactiveProperty<int> MaxHealth = new();
        public readonly ReactiveProperty<bool> IsAlive = new();
        
        public void Init(UnitModel model)
        {
            _view = model.View;
            _config = model.Config;
            _collider = model.Collider;
            _animator = model.Animator; 
            IsAlive.Value = true;
            MaxHealth.Value = _config.UnitStats[EUnitStat.Health];
            CurrentHealth.Value = MaxHealth.Value;
        }

        public void AddHealth(int value)
        {
            CurrentHealth.Value += value;
            CurrentHealth.Value = Math.Clamp(CurrentHealth.Value, 1, MaxHealth.Value);
        }

        public void GetDamage(int value)
        {
            CurrentHealth.Value -= value;
            _animator.SetTrigger(EAnimationType.GetDamage);

            if (CurrentHealth.Value <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            IsAlive.Value = false;
            _animator.SetTrigger(EAnimationType.Die);
            CurrentHealth.Value = 0;
            
            _collider.size = new Vector2(0.3F,0.3F);
            OnDeath();
        }

        public virtual void OnDeath(){}
    }
}