using Scripts.Configs;
using UnityEngine;

namespace Units
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private UnitConfig _config;

        public UnitConfig Config => _config;
    }
}