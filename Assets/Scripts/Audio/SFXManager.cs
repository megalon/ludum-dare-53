using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField]
    private SFXSpawner _enemyDiedSFXSpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void EnemyDiedSFX()
    {
        _enemyDiedSFXSpawner.Spawn();
    }
}
