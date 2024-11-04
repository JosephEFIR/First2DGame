using AudioObjectLib;
using UnityEngine;

namespace Audio
{
    public class PhysicItemAudio : MonoBehaviour
    {
        [Header("Аудио клип для столкновения с объектами")]
        [SerializeField] private AudioClip _audioClip;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                var audioObject = AudioDataManager.Manager.CreateAudioObject(transform.position, _audioClip);
                audioObject.RandomPitch(1,1.4F);
                audioObject.Play();
                audioObject.RemoveIFNotPlaying();
            }
        }
    }
}