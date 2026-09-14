using Scripts.Configs;
using Scripts.TriggerScripts;
using UnityEngine;

namespace Units.Enemies
{
    public class EnemyView : UnitView
    {
        [SerializeField] private AI_Config ai_Config;
        [SerializeField] private JumpTrigger jumpTrigger;
        public AI_Config AI_Config => ai_Config;
        public JumpTrigger JumpTrigger => jumpTrigger;
    }
}