using Scripts.Configs;
using Scripts.Enemies;
using Scripts.Player;
using Scripts.TriggerScripts;
using Scripts.Units;

namespace Game.Units.Enemies
{
    public class EnemyModel : UnitModel
    {
        public PlayerView Target;
        public AI_Config AI_Config;
        public EnemyAIController AI_Controller;
        public JumpTrigger JumpTrigger;
    }
}