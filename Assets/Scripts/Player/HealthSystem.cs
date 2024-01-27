using System;
using Scripts.Configs;
using UnityEngine;
using Scripts.Player;
using Zenject;

namespace Scripts.Managers
{
    public class HealthSystem : MonoBehaviour
    { 
        [Inject]private EventManager _eventManager;
        [Inject] private PlayerConfig _playerConfig;

        private Animator _animator;
        private PlayerController _playerController;
        private CapsuleCollider2D _colliderSize;

        private int _health;

        public bool IsDead { get; private set; }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _colliderSize = GetComponent<CapsuleCollider2D>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            AddHealth(_playerConfig.Health);
            IsDead = false;
        }

        public void AddHealth(int value)
        {
            _health += value;
            _health = Math.Clamp(_health, 1 , _playerConfig.MaxHealth);

            _eventManager.AddHealthUI(_health);
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            _eventManager.TakeDamageUI(damage);
            if (_health <= 0)
            {
                Death();
            }
        }
        private void Death()
        {
            _health = 0;
            IsDead = true;
            
            _colliderSize.size = new Vector2(0.2F,0.2F);
            
            _playerController.Stay(true);
            _animator.SetBool("isDead", true);
            
            _eventManager.DeathUI();
        }
        
    }
}