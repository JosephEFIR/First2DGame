using Health;
using Scripts.Animators;
using Scripts.Configs;
using Scripts.Player;
using Scripts.Weapons.Melee;
using Units;
using UnityEngine;

namespace Scripts.Units
{
    public class UnitModel
    {
        public UnitConfig Config;
        public UnitView View;
        public MeleePoint MeleePoint;
        public StateMachine StateMachine;
        public HealthComponent HealthComponent;
        public CustomAnimator Animator;

        public Transform Transform;
        public Rigidbody2D Rigidbody;
        public CapsuleCollider2D Collider;
        public GroundCheck GroundCheck;
    }
}