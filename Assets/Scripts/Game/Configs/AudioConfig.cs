using ProjectTools;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "LocalAudioConfig", menuName = "Configs/Audio")]
    
    public class AudioConfig : ScriptableObject
    { 
        [SerializeField]private SerializableDictionary<EClipType, AudioClip> _audios = new();
        public SerializableDictionary<EClipType, AudioClip> Audios => _audios;
    }
}