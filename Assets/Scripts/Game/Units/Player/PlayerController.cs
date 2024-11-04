using Scripts.Animators;
using Scripts.Health;
using Units;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(
        typeof(CustomAnimator),
        typeof(PlayerMoveController),
        typeof(PlayerHealth))]
    public class PlayerController : UnitController
    {
        
    }
}