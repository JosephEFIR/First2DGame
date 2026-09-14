using Game.Animators;
using Scripts.Animators;
using Scripts.Enemies;
using Scripts.Health;
using Scripts.Player;
using Scripts.Weapons.Melee;
using Units.Enemies;
using UnityEngine;
using Zenject;

namespace Game.Units.Enemies
{
    public class EnemyInit : MonoBehaviour
    {
        [Inject] private PlayerView _playerView;
        private EnemyView _view;
        private UnitAnimationEventReceiver _unitAnimationEventReceiver;
        
        private EnemyAIController _aiController = new();
        private EnemyHealth _health;
        private EnemyMeleeAttack _meleeAttack = new();
        private CustomAnimator _animator = new ();
      
        private void Awake()
        {
            _view = GetComponent<EnemyView>();
            _health = GetComponent<EnemyHealth>();
            _unitAnimationEventReceiver = GetComponent<UnitAnimationEventReceiver>();
        }

        private void Start()
        {
            EnemyModel unitmodel = new();
            unitmodel.View = _view;
            unitmodel.Transform = _view.transform;
            unitmodel.Rigidbody = _view.Rigidbody2D;
            unitmodel.Collider = _view.CapsuleCollider2D;
            unitmodel.Config = _view.Config;
            unitmodel.GroundCheck = _view.GroundCheck;
            unitmodel.MeleePoint = _view.MeleePoint;
            unitmodel.JumpTrigger = _view.JumpTrigger;
            unitmodel.AI_Config = _view.AI_Config;
            unitmodel.Target = _playerView;
            
            
            //animator
            unitmodel.Animator = _animator;
            _animator.Init(_view.Animator);
            //ai controller
            unitmodel.AI_Controller = _aiController;
            _aiController.Init(unitmodel);
            _meleeAttack.Init(unitmodel);
            //health
            unitmodel.HealthComponent = _health;
            _health.Init(unitmodel);
            
            _unitAnimationEventReceiver.Init(_meleeAttack, _health);
        }

        private void Update()
        {
            _aiController.Run();
            _meleeAttack.Run();
        }

        private void OnDestroy()
        {
            
        }
    }
}