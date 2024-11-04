using System;
using Assets.Scripts.Interfaces;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using UniRx;
using Units;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(CustomAnimator))]
    public abstract class HealthComponent : MonoBehaviour,IHealthSystem
    {
        private UnitConfig _config;
        private UnitController _controller;

        private CustomAnimator _animator;

        public readonly ReactiveProperty<int> CurrentHealth = new();
        public readonly ReactiveProperty<int> MaxHealth = new();
        public readonly ReactiveProperty<bool> IsAlive = new();

        private CompositeDisposable _disposable = new();

        private void Awake()
        {
            _controller = GetComponent<UnitController>();
            _animator = GetComponent<CustomAnimator>();
            _config = _controller.Config;
        }


        private void Start()
        {
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

            CapsuleCollider2D colliderSize = _controller.GetComponent<CapsuleCollider2D>();
            colliderSize.size = new Vector2(0.3F,0.3F);
        }

        public abstract void OnDeath();

        private void OnDestroy()
        {
            _disposable?.Clear();
        }
    }
}