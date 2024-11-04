using System;
using UnityEngine;

namespace Scripts.TriggerScripts
{
    public class BallModeTrigger : MonoBehaviour
    {
        [NonSerialized] public bool TriggerOn = false;
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.tag == "Ground")
            {
                TriggerOn = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.tag == "Ground")
            {
                TriggerOn = false;
            }
        }
    }
}