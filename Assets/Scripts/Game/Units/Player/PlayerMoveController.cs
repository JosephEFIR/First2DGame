using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.TriggerScripts;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerMoveController
    {
        private bool _blockPlayer; 
        private GroundCheck _groundCheck;
        private BallModeTrigger _ballModeTrigger;
        
        private Rigidbody2D _rigidbody2D;
        private CustomAnimator _animator;
        private CapsuleCollider2D _colliderSize;

        private float _horizontalAxis;
        private bool _isFacingRight = true;
        
        private Vector2 _defaultColliderSize;

        private UnitConfig _config;
        private Transform _transform;
        private float _speed;
        private float _jumpForce;
        
        public void Init(PlayerModel model)
        {
            _config = model.Config;
            _transform = model.Transform;
            _animator = model.Animator;
            _rigidbody2D = model.Rigidbody;
            _colliderSize = model.Collider;
            _ballModeTrigger = model.BallModeTrigger;
            _groundCheck = model.GroundCheck;
            
            _defaultColliderSize = _colliderSize.size;
            _speed = _config.UnitStats[EUnitStat.Speed];
            _jumpForce = _config.UnitStats[EUnitStat.JumpForce];
        }

        public void Run()
        {
            if (_blockPlayer)
            {
                return;
            }
            
            _horizontalAxis = Input.GetAxis("Horizontal");
            if (_groundCheck.IsGround)
            {
                    
                if (_ballModeTrigger.TriggerOn)
                {
                    BallMode();
                }
                else
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        BallMode();
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        Jump();
                    }
                    else
                    {
                        Move();
                    }
                }
            }
            if (_groundCheck.IsGround == false)
            {
                if (_ballModeTrigger.TriggerOn)
                {
                    BallMode();
                }
                else
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        BallMode();
                    }
                    else
                    {
                        Move();
                        Landing();
                    }
                }
            }
            FlipX();
        }
        
        private void Move()
        {
            _animator.SetMoveSpeed(_rigidbody2D.linearVelocity.magnitude);
            _animator.SetBool(EAnimationType.BallMode, false); //TODO FIX THIS
            
            _rigidbody2D.linearVelocity = new Vector2(_horizontalAxis * _speed, _rigidbody2D.linearVelocity.y);
            _colliderSize.size = new Vector2(_defaultColliderSize.x, _defaultColliderSize.y);
        }
        private void BallMode()
        {
            _animator.SetBool(EAnimationType.BallMode ,true);
            
            _rigidbody2D.linearVelocity = new Vector2(_horizontalAxis * _speed * 1.5F, _rigidbody2D.linearVelocity.y);
            _colliderSize.size = new Vector2(0.45F, 0.35F); //TODO HARD CODE
        }
        
        private void Jump()
        {
            _animator.SetTrigger(EAnimationType.Jump);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        
        private void Landing()
        {
            if (_rigidbody2D.linearVelocity.y < -.1f)
            {
                _animator.SetTrigger(EAnimationType.Landing);
            }
        }
        
        public void Block(bool value = true)
        {
            _blockPlayer = value;
        }
        
        private void FlipX() //TODO <- MAYBE UTILS? NO! 11.09.2024
        {
            if (_isFacingRight && _horizontalAxis < 0f || !_isFacingRight && _horizontalAxis > 0f)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 localScale = _transform.localScale;
                localScale.x *= -1f;
                _transform.localScale = localScale;
            }
        }
    }
}