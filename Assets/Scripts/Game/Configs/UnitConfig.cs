using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Configs
{
    public enum EUnitStat
    {
        Health,
        Damage,
        JumpForce,
        Speed
    }
    
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/UnitConfig")]
    public class UnitConfig : SerializedScriptableObject
    {
        [ListDrawerSettings(ShowFoldout = true, DraggableItems = false, HideRemoveButton = true)]
        
        [SerializeField]
        private Dictionary<EUnitStat, int> _unitStats = new()
        {
            {EUnitStat.Health, 0},
            {EUnitStat.Damage, 0},
            {EUnitStat.JumpForce, 0},
            {EUnitStat.Speed, 0},
        };

        public Dictionary<EUnitStat, int> UnitStats => _unitStats;
    }
}