using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AudioSourceData")]
    public class AudioSourceData : ScriptableObject
    {
        [Range(0,225)]public int Priority = 128;
        public AudioMixerGroup _outputAudioMixerGroup;
        [Range(0, 1)] public float Volume=1;
        [Range(-3, 3)] public float Pitch=1;
        [Range(-1, 1)] public float StereoPan = 0;
        [Range(0, 1)] public float SpatialBlend = 0;
        [Range(0, 1.1f)] public float ReverbZoneMix = 1;
        public float MinDistance = 1;
        public float MaxDistance = 500;
    }
}
