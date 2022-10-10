using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _baseCapturedClip;
    [SerializeField] private AudioClip _baseLostClip;
    [SerializeField] private AudioClip _baseUpgradedClip;

    public void PlayCaptureClip()
    {
        _audioSource.clip = _baseCapturedClip;
        _audioSource.Play();
    }

    public void PlayLostClip()
    {
        _audioSource.clip = _baseLostClip;
        _audioSource.Play();
    }

    public void PlayUpgradeClip()
    {
        _audioSource.clip = _baseUpgradedClip;
        _audioSource.Play();
    }
}
