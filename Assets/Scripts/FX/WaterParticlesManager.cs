using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class WaterParticlesManager : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> _particleSystems;

    public void StopParticleSystems()
    {
        foreach(ParticleSystem ps in _particleSystems)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void StartParticleSystems()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }
    }
}
