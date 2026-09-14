using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Configs
{
    [System.Serializable]
    public class UnitStatPair
    {
        [SerializeField] private EUnitStat _key;
        [SerializeField] private int _value;

        public EUnitStat Key => _key;
        public int Value => _value;
        
        public UnitStatPair(EUnitStat key, int value)
        {
            _key = key;
            _value = value;
        }
    }

    public enum EUnitStat
    {
        Health,
        Damage,
        JumpForce,
        Speed
    }

    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField]
        private List<UnitStatPair> _unitStats = new()
        {
            new UnitStatPair(EUnitStat.Health, 0),
            new UnitStatPair(EUnitStat.Damage, 0),
            new UnitStatPair(EUnitStat.JumpForce, 0),
            new UnitStatPair(EUnitStat.Speed, 0),
        };

        public Dictionary<EUnitStat, int> UnitStats
        {
            get
            {
                var dict = new Dictionary<EUnitStat, int>();
                foreach (var pair in _unitStats)
                {
                    if (!dict.ContainsKey(pair.Key))
                        dict.Add(pair.Key, pair.Value);
                }
                return dict;
            }
        }
    }
}