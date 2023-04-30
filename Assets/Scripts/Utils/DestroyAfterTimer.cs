using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimer : Timer
{
    private void Awake()
    {
        TimerComplete.AddListener(DestroyAfterTime);
    }

    private void DestroyAfterTime()
    {
        Destroy(gameObject);
    }
}
