using Audio;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.TriggerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private BallModeTrigger _ballModeTrigger;
        
        private Rigidbody2D _rigidbody2D;
        private CustomAnimator _animator;
        private LocalAudioService _audioService;
        private CapsuleCollider2D _colliderSize;

        private float _horizontalAxis;
        private bool _isFacingRight = true;

        private bool _stopWalk = false;

        private Vector2 _defaultColliderSize;

        public PlayerConfig Config => _playerConfig;

        private void Awake()
        {
            _animator = GetComponent<CustomAnimator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioService = GetComponent<LocalAudioService>();
            _colliderSize = GetComponent<CapsuleCollider2D>();
            
            _defaultColliderSize = _colliderSize.size;
        }

        private void FixedUpdate()
        {
            if (_stopWalk == false)
            {
                _horizontalAxis = Input.GetAxis("Horizontal");
                if (_groundCheck.IsGround)
                {
                    
                    if (_ballModeTrigger.TriggerOn)//TODO FIX THIS ON 1.4
                    {
                        BallMode();
                    }
                    else
                    {
                        _animator.SetLanding(false);
                        if (Input.GetKey(KeyCode.Space))
                        {
                            BallMode();
                        }
                        else if (Input.GetKey(KeyCode.W))
                        {
                            Jump();
                        }
                        else
                        {
                            Move();
                        }
                    }
                }
                else if (_groundCheck.IsGround == false)
                {
                    if (_ballModeTrigger.TriggerOn)//TODO FIX THIS ON 1.4
                    {
                        BallMode();
                    }
                    else
                    {
                        _animator.SetLanding(true);
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
        }
        
        private void Move()
        {
            _animator.SetMoveSpeed(_rigidbody2D.velocity.magnitude);
            _animator.SetRun(false);
            
            _rigidbody2D.velocity = new Vector2(_horizontalAxis * _playerConfig.WalkSpeed, _rigidbody2D.velocity.y);
            _colliderSize.size = new Vector2(_defaultColliderSize.x, _defaultColliderSize.y);
        }
        private void BallMode()
        {
            _animator.SetRun(true);
            
            _rigidbody2D.velocity = new Vector2(_horizontalAxis * _playerConfig.WalkSpeed * 1.5F, _rigidbody2D.velocity.y);
            _colliderSize.size = new Vector2(0.45F, 0.35F);
        }
        
        private void Jump()
        {
            _audioService.Play(EClipType.Jump);
            _animator.Play(EAnimationType.Jump);
            _rigidbody2D.AddForce(Vector2.up * _playerConfig.JumpForce, ForceMode2D.Impulse);
        }
        
        private void Landing()
        {
            if (_rigidbody2D.velocity.y < -.1f)
            {
                _animator.Play(EAnimationType.Landing);
            }
        }
        
        public void Stay(bool value)
        {
            _stopWalk = value;
        }
        
        private void FlipX() //TODO <- MAYBE UTILS?
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