using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Configs
{
    [System.Serializable]
    public class AIStatPair
    {
        [SerializeField] private EaiStats _key;
        [SerializeField] private int _value;

        public EaiStats Key => _key;
        public int Value => _value;

        public AIStatPair(EaiStats key, int value)
        {
            _key = key;
            _value = value;
        }
    }

    public enum EaiStats
    {
        VisibleDistance,
        StopDistance,
    }

    [CreateAssetMenu(fileName = "AI_Config", menuName = "Configs/AI_Config")]
    public class AI_Config : ScriptableObject
    {
        [SerializeField]
        private List<AIStatPair> _aiStats = new()
        {
            new AIStatPair(EaiStats.VisibleDistance, 0),
            new AIStatPair(EaiStats.StopDistance, 0),
        };

        public Dictionary<EaiStats, int> AIStats
        {
            get
            {
                var dict = new Dictionary<EaiStats, int>();
                foreach (var pair in _aiStats)
                {
                    if (!dict.ContainsKey(pair.Key))
                        dict.Add(pair.Key, pair.Value);
                }
                return dict;
            }
        }
    }
}