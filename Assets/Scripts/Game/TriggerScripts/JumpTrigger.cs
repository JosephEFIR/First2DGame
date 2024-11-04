using UnityEngine;

namespace Scripts.TriggerScripts
{
    public class JumpTrigger : MonoBehaviour
    {
        public bool CanJump { get; private set; }
            
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.tag == "Ground")
            {
                CanJump = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.tag == "Ground")
            {
                CanJump = true;
            }
        }
    }
}