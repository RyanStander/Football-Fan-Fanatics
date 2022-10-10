using UnityEngine;

namespace Audio
{
    public class PlaySoundFromAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void OnValidate()
        {
            _audioSource.clip = _audioClip;
        }

        public void PlaySound()
        {
            if (_audioSource==null) return;
            
            _audioSource.Play();
        }
    }
}
