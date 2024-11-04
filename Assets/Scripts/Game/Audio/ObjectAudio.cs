using AudioObjectLib;
using NaughtyAttributes;
using UnityEngine;

namespace Audio
{
    public class ObjectAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [Range(0,1)]
        [SerializeField] private float volume;
        
        [SerializeField] private float zone;
        private void Start()
        {
            var audioObject = AudioDataManager.Manager.CreateAudioObject(transform.position, _audioClip);
            audioObject.GetAudioSource().loop = true;
            audioObject.GetAudioSource().volume = volume;
            audioObject.GetAudioSource().minDistance = 1;
            audioObject.GetAudioSource().maxDistance = zone;
            audioObject.Play();
        }
    }
}