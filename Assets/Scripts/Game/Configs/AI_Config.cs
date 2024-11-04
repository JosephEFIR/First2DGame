using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Configs
{
    public enum EaiStats //а по другому никак не назвать
    {
        VisibleDistance,
        StopDistance,
    }
    
    [CreateAssetMenu(fileName = "AI_Config", menuName = "Configs/AI_Config")]
    public class AI_Config : SerializedScriptableObject
    {
        [ListDrawerSettings(ShowFoldout = true, DraggableItems = false, HideRemoveButton = true)] 
        [SerializeField] private Dictionary<EaiStats, int> _aiStats = new();

        public Dictionary<EaiStats, int> AIStats => _aiStats;
    }
}