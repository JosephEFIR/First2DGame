using Scripts.Configs;
using Scripts.Player;
using Scripts.TriggerScripts;
using Scripts.Weapons.Melee;
using UnityEngine;

namespace Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private UnitConfig config;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private CapsuleCollider2D capsuleCollider2D;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private MeleePoint meleePoint;
        public UnitConfig Config => config;
        public Animator Animator => animator;
        public Rigidbody2D Rigidbody2D => rigidbody2D;
        public CapsuleCollider2D CapsuleCollider2D => capsuleCollider2D;
        public GroundCheck GroundCheck => groundCheck;
        public MeleePoint MeleePoint => meleePoint;
    }
}