using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager Instance;

    [SerializeField]
    private FXSpawner _deathFXSpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void EnemyDied(Vector3 position)
    {
        GameObject obj = _deathFXSpawner.Spawn();
        obj.transform.position = position;
    }
}
