using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public UnityEvent OnPaused;
    public UnityEvent OnUnPaused;

    public Action OnPausedAction;
    public Action OnUnPausedAction;

    private bool _paused = false;
    public bool IsPaused { get => _paused; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void TogglePause()
    {
        if (_paused)
        {
            UnPause();
        } else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_paused) return;

        _paused = true;

        Time.timeScale = 0;

        OnPaused.Invoke();
        OnPausedAction();
    }

    public void UnPause()
    {
        if (!_paused) return;

        _paused = false;

        Time.timeScale = 1;

        OnUnPaused.Invoke();
        OnUnPausedAction();
    }
}
