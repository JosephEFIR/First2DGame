using Game.Units.Enemies;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using UnityEngine;
using Scripts.Player;
using Scripts.TriggerScripts;

namespace Scripts.Enemies
{
    public class EnemyAIController
    {
        private AI_Config _aiConfig;
        private bool _blockEnemy;
        
        private GroundCheck _groundCheck;
        private JumpTrigger _jumpTrigger;
        
        private PlayerView _target;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private CustomAnimator _customAnimator;
        
        private UnitConfig _config;
        private float _speed;
        private float _jumpForce;
        private float _distanceToTarget;
        private float _visibleDistance;
        private float _stopDistance;
        
        private bool _isFacingRight = true;
        

        public void Init(EnemyModel model)
        {
            _config = model.Config;
            _aiConfig = model.AI_Config;
            _transform = model.Transform;
            _rigidbody2D = model.Rigidbody;
            _customAnimator = model.Animator;
            _groundCheck = model.GroundCheck;
            _jumpTrigger = model.JumpTrigger;
            _target = model.Target;
            _speed = _config.UnitStats[EUnitStat.Speed];
            _jumpForce = _config.UnitStats[EUnitStat.JumpForce];
            _visibleDistance = _aiConfig.AIStats[EaiStats.VisibleDistance];
            _stopDistance = _aiConfig.AIStats[EaiStats.StopDistance];
            
            _customAnimator.SetTrigger(EAnimationType.Idle);
        }

        public void Run()
        {
            if (!_blockEnemy)
            {
                if (_groundCheck.IsGround)
                {
                    _distanceToTarget = Vector3.Distance(_target.transform.position, _transform.position);
                    if (_distanceToTarget <= _visibleDistance && _distanceToTarget > _stopDistance)
                    {
                        Move();
                    }
                    else _customAnimator.SetTrigger(EAnimationType.Idle);
                }
                if (_groundCheck.IsGround & _jumpTrigger.CanJump)
                {
                    Jump();
                }
                FlipX();
            }
        }

        private void Move()
        {
            _customAnimator.SetMoveSpeed(_rigidbody2D.linearVelocity.magnitude);
            
            if (_target.transform.position.x < _transform.position.x)
            {
                _rigidbody2D.linearVelocity = new Vector2(- _speed, 0);
                _isFacingRight = true;
            }
            else if (_target.transform.position.x > _transform.position.x)
            {
                _rigidbody2D.linearVelocity = new Vector2(_speed, 0);
                _isFacingRight = false;
            }
        }

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce  ,ForceMode2D.Impulse);
        }

        public void Stay(bool value)
        {
            _blockEnemy = value;
        }
        
        private void FlipX()
        {
            if (_isFacingRight)
            {
                Vector3 localScale = _transform.localScale;
                localScale.x = -1f;
                _transform.localScale = localScale;
            }
            else
            {
                Vector3 localScale = _transform.localScale;
                localScale.x = 1f;
                _transform.localScale = localScale;
            }
        }
    }
}