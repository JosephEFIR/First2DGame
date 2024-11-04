using UnityEngine;

namespace Audio
{
    public class CaveReverbZone : MonoBehaviour
    {
        [SerializeField] private AudioReverbZone _audio;

        private void Start()
        {
            _audio.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                _audio.enabled = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                _audio.enabled = false;
            }
        }
    }
}