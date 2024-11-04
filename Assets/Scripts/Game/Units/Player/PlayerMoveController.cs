using Audio;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.TriggerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    public class PlayerMoveController : MonoBehaviour 
    {
        [SerializeField] private bool _blockPlayer;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private BallModeTrigger _ballModeTrigger;
        
        private Rigidbody2D _rigidbody2D;
        private CustomAnimator _animator;
        private LocalAudioService _audioService;
        private CapsuleCollider2D _colliderSize;

        private float _horizontalAxis;
        private bool _isFacingRight = true;
        
        private Vector2 _defaultColliderSize;

        private UnitConfig _config;
        private float _speed;
        private float _jumpForce;

        private void Awake()
        {
            PlayerController player = GetComponent<PlayerController>();
            _config = player.Config;
            
            _animator = GetComponent<CustomAnimator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioService = GetComponent<LocalAudioService>();
            _colliderSize = GetComponent<CapsuleCollider2D>();
            
        }

        private void Start()
        {
            _defaultColliderSize = _colliderSize.size;
            _speed = _config.UnitStats[EUnitStat.Speed];
            _jumpForce = _config.UnitStats[EUnitStat.JumpForce];
        }

        private void Update()
        {
            if (_blockPlayer)
            {
                return;
            }
            
            _horizontalAxis = Input.GetAxis("Horizontal");
            if (_groundCheck.IsGround)
            {
                    
                if (_ballModeTrigger.TriggerOn)//TODO FIX THIS ON 1.6
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
            _animator.SetMoveSpeed(_rigidbody2D.velocity.magnitude);
            _animator.SetBool(EAnimationType.BallMode, false); //TODO FIX THIS
            
            _rigidbody2D.velocity = new Vector2(_horizontalAxis * _speed, _rigidbody2D.velocity.y);
            _colliderSize.size = new Vector2(_defaultColliderSize.x, _defaultColliderSize.y);
        }
        private void BallMode()
        {
            _animator.SetBool(EAnimationType.BallMode ,true);
            
            _rigidbody2D.velocity = new Vector2(_horizontalAxis * _speed * 1.5F, _rigidbody2D.velocity.y);
            _colliderSize.size = new Vector2(0.45F, 0.35F); //TODO HARD CODE
        }
        
        private void Jump()
        {
            _audioService.Play(EClipType.Jump);
            _animator.SetTrigger(EAnimationType.Jump);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        
        private void Landing()
        {
            if (_rigidbody2D.velocity.y < -.1f)
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
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        private void OnWalking()
        {
            _audioService.PlayPitch(EClipType.Walk,Random.Range(1,2));
        }
    }
}