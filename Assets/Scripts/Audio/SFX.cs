using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [Range(0, 1)]
    public float pitchRandomization;
    public bool playOnEnable;
    public bool pauseWithGame = true;

    private AudioSource _audioSource;
    private bool _wasPlayingWhenPaused;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (pauseWithGame)
        {
            PauseManager.Instance.OnPausedAction += HandleGamePaused;
            PauseManager.Instance.OnUnPausedAction += HandleGameUnPaused;
        }
    }

    private void HandleGamePaused()
    {
        _wasPlayingWhenPaused = _audioSource.isPlaying;
        _audioSource.Pause();
    }

    private void HandleGameUnPaused()
    {
        if (_wasPlayingWhenPaused)
        {
            _audioSource.Play();
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            Play();
        }
    }

    public void Play()
    {
        if (pitchRandomization > 0)
        {
            _audioSource.pitch = 1 + Random.Range(-pitchRandomization, pitchRandomization);
        }

        _audioSource.Play();
    }

    public bool IsPlaying()
    {
        return _audioSource.isPlaying;
    }


    private void OnDestroy()
    {
        if (pauseWithGame)
        {
            PauseManager.Instance.OnPausedAction -= HandleGamePaused;
            PauseManager.Instance.OnUnPausedAction -= HandleGameUnPaused;
        }
    }
}
