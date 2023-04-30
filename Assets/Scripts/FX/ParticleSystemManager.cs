using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager Instance;

    [SerializeField]
    private FXSpawner _explosionFXSpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void Explosion(Vector3 position)
    {
        GameObject obj = _explosionFXSpawner.Spawn();
        obj.transform.position = position;
    }
}
