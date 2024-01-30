using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume = .5f;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _volume;

        _volume = PlayerPrefs.GetFloat("musicVolume", .5f);
    }
    public void IncreaseVolume()
    {
        _volume += .1f;
        _volume = Mathf.Clamp01(_volume);
        _audioSource.volume = _volume;
        SaveMusicVolume(_volume);
    }
    public void DecreaseVolume()
    {
        _volume -= .1f;
        _volume = Mathf.Clamp01(_volume);
        _audioSource.volume = _volume;
        SaveMusicVolume(_volume);
    }
    public float GetVolume() => _volume;
    private void SaveMusicVolume(float volume) => PlayerPrefs.SetFloat("musicVolume", volume);
}

