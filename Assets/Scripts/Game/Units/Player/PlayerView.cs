using Scripts.TriggerScripts;
using Units;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerView : UnitView
    {
        [SerializeField] private BallModeTrigger ballmodeTrigger;
        public BallModeTrigger BallModeTrigger => ballmodeTrigger;
    }
}