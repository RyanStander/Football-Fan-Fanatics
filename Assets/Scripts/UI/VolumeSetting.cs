using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class VolumeSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        public void SetVolume(float volume)
        {
            _audioMixer.SetFloat("MasterVolume",volume);
        }
    }
}
