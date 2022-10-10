using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class UnitSoundEffects : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> fightSFXs;
        [SerializeField] private float _sfxLifetime;
        [SerializeField] private AudioSourceData _audioSourceData;
    
        public void SpawnAudioSFX()
        {
            //spawn audio source
            var sfxObj = new GameObject();
            var audioSource = sfxObj.AddComponent<AudioSource>();
            
            //play sound
            audioSource.clip = fightSFXs[Random.Range(0, fightSFXs.Count)];
            audioSource.gameObject.name = $"SFX({audioSource.clip.name})";
            audioSource.Play();
            
            #region SetAudioSourceData
            audioSource.priority = _audioSourceData.Priority;
            audioSource.volume = _audioSourceData.Volume;
            audioSource.pitch = _audioSourceData.Pitch;
            audioSource.outputAudioMixerGroup = _audioSourceData._outputAudioMixerGroup;
            audioSource.panStereo = _audioSourceData.StereoPan;
            audioSource.spatialBlend = _audioSourceData.SpatialBlend;
            audioSource.reverbZoneMix = _audioSourceData.ReverbZoneMix;
            audioSource.minDistance = _audioSourceData.MinDistance;
            audioSource.maxDistance = _audioSourceData.MaxDistance;

            #endregion

            //attach a self destruct script
            var selfDestruct = sfxObj.AddComponent<SelfDestruct>();
            selfDestruct.InitializeSelfDestruct(_sfxLifetime);
        }
    }
}
