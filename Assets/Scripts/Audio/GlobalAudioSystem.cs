﻿using DG.Tweening;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Audio
{
    public class GlobalAudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource _forestAudio;
        [SerializeField] private AudioSource _caveAudio;
        [SerializeField] private AudioSource _musicAudio;

        public static GlobalAudioSystem Instance;

        private void Awake()
        {
            Instance = GetComponent<GlobalAudioSystem>();
        }

        public void Play(EClipType type)
        {
            switch (type)
            {
                case EClipType.None:
                    Debug.LogWarning("АудиоКлип не выбран");
                    break;
                case EClipType.Forest:
                    _forestAudio.DOFade(1,0.5F);
                    _forestAudio.Play();
                    break;
                case EClipType.Cave:
                    _caveAudio.DOFade(1, 0.5f);
                    _caveAudio.Play();
                    break;
                case EClipType.Music:
                    _musicAudio.DOFade(1, 0.5f);
                    _musicAudio.Play();
                    break;
            }
        }

        public void Stop(EClipType type)
        {
            switch (type)
            {
                case EClipType.None:
                    Debug.LogWarning("АудиоКлип не выбран");
                    break;
                case EClipType.Forest:
                    _forestAudio.DOFade(0,0.5F);
                    _forestAudio.Stop();
                    break;
                case EClipType.Cave:
                    _caveAudio.DOFade(0, 0.5f);
                    _caveAudio.Stop();
                    break;
                case EClipType.Music:
                    _musicAudio.DOFade(0, 0.5f);
                    _musicAudio.Stop();
                    break;
            }
        }
    }
}