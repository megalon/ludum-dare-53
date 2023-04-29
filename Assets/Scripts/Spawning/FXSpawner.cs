using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : SpawnerPooled
{
    protected override void OnDestroyPoolObject(GameObject obj)
    {
    }

    protected override void OnReturnedToPool(GameObject obj)
    {
    }

    protected override void OnTakeFromPool(GameObject obj)
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }
}
