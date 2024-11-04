using Audio;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Enums;
using Scripts.Health;
using UnityEngine;
using Scripts.Player;
using Scripts.TriggerScripts;
using Sirenix.OdinInspector;
using Units.Enemies;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts.Enemies
{
    [RequireComponent(
        typeof(Rigidbody2D),
        typeof(CustomAnimator), 
        typeof(EnemyHealth))]
    public class EnemyAI : MonoBehaviour
    {
        [LabelText("AI CONFIG")] 
        [SerializeField] private AI_Config _aiConfig;
        
        [LabelText("Блокировать AI врага")]
        [SerializeField] private bool _blockEnemy;
        
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private JumpTrigger _jumpTrigger;
        
        [Inject] private PlayerController _playerController;
        
        private Rigidbody2D _rigidbody2D;
        private CustomAnimator _customAnimator;
        private LocalAudioService _audioService;
        private EnemyController _enemyController;
        
        private float _speed;
        private float _jumpForce;
        private float _distanceToTarget;
        private float _visibleDistance;
        
        private bool _isFacingRight = true;


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _customAnimator = GetComponent<CustomAnimator>();
            _audioService = GetComponent<LocalAudioService>();
            _enemyController = GetComponent<EnemyController>();
        }

        private void Start()
        {
            _speed = _enemyController.Config.UnitStats[EUnitStat.Speed];
            _jumpForce = _enemyController.Config.UnitStats[EUnitStat.JumpForce];
            _visibleDistance = _aiConfig.AIStats[EaiStats.VisibleDistance];

            _customAnimator.SetTrigger(EAnimationType.Idle);
        }

        private void FixedUpdate()
        {
            if (!_blockEnemy)
            {
                if (_groundCheck.IsGround)
                {
                    _distanceToTarget = Vector3.Distance(_playerController.transform.position, gameObject.transform.position);
                    if (_distanceToTarget <= _visibleDistance)
                    {
                        Move();
                    }
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
            _customAnimator.SetMoveSpeed(_rigidbody2D.velocity.magnitude);
            
            if (_playerController.transform.position.x < gameObject.transform.position.x)
            {
                _rigidbody2D.velocity = new Vector2(- _speed, 0);
                _isFacingRight = true;
            }
            else if (_playerController.transform.position.x > gameObject.transform.position.x)
            {
                _rigidbody2D.velocity = new Vector2(_speed, 0);
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
                Vector3 localScale = transform.localScale;
                localScale.x = -1f;
                transform.localScale = localScale;
            }
            else
            {
                Vector3 localScale = transform.localScale;
                localScale.x = 1f;
                transform.localScale = localScale;
            }
        }

        private void OnWalking()
        {
            _audioService.PlayPitch(EClipType.Walk, Random.Range(1,2));
        }
    }
}