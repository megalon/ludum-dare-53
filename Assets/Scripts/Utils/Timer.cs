using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    protected float _lifetime = 1;
    protected float _timer = 0;

    [SerializeField]
    protected UnityEvent TimerComplete;

    public void OnEnable()
    {
        _timer = _lifetime;
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            TimerComplete.Invoke();
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
