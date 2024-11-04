using Scripts.Configs;
using Scripts.Enums;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class LocalAudioService : MonoBehaviour
    {
        [SerializeField] private AudioConfig _config;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void Play(EClipType type)
        {
            _audioSource.clip = _config.Audios[type];
            _audioSource.Play();
        }

        public void Stop(EClipType type) //Мало ли
        {
            _audioSource.clip = _config.Audios[type];
            _audioSource.Stop();
        }

        public void PlayPitch(EClipType type, float pitch)
        {
            _audioSource.clip = _config.Audios[type];
            _audioSource.pitch = pitch;
            _audioSource.Play();
        }
    }
}