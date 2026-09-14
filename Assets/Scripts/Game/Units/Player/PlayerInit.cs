using Game.Animators;
using Scripts.Animators;
using Scripts.Health;
using Scripts.Weapons.Melee;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInit : MonoBehaviour
    {
        private PlayerView _view;
        private PlayerHealth _health;
        private UnitAnimationEventReceiver _unitAnimationEventReceiver;

        private PlayerMoveController _moveController = new();
        private MeleeAttackController _meleeAttackController = new();
        private CustomAnimator _animator = new();
        
        public PlayerModel Model {get; private set;} = new();
        
        private void Awake()
        {
            _view = GetComponent<PlayerView>();
            _health = GetComponent<PlayerHealth>();
            _unitAnimationEventReceiver = GetComponent<UnitAnimationEventReceiver>();
        }

        private void Start()
        {
            Model.View = _view;
            Model.Transform = _view.transform;
            Model.Config = _view.Config;
            Model.Rigidbody = _view.Rigidbody2D;
            Model.Collider = _view.CapsuleCollider2D;
            Model.BallModeTrigger = _view.BallModeTrigger;
            Model.GroundCheck = _view.GroundCheck;
            Model.MeleePoint = _view.MeleePoint;

            Model.StateMachine = new();
            
            //Animator
            Model.Animator = _animator;
            _animator.Init(_view.Animator);
            //Move
            _moveController.Init(Model);
            //Health
            Model.HealthComponent = _health;
            _health.Init(Model);
            //Attack
            _meleeAttackController.Init(Model);
            
            _unitAnimationEventReceiver.Init(_meleeAttackController, _health);
        }

        private void Update()
        {
            _moveController.Run();
            _meleeAttackController.Run();
        }

        private void OnDestroy()
        {
            _meleeAttackController.OnDestroy();
        }
    }
}