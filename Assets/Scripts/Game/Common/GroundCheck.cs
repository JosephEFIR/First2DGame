using Audio;
using UnityEngine;

namespace Scripts.Player
{
    public class GroundCheck : MonoBehaviour
    {
        private bool _isGround;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground") & _rigidbody2D.linearVelocity.y <= 0)
            {
                _isGround = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGround = false;
            }
        }

        public bool IsGround => _isGround;
    }
}