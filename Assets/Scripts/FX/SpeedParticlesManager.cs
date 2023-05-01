using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticlesManager : MonoBehaviour
{
    [SerializeField]
    private WaterParticlesManager _waterParticlesManager;

    [SerializeField]
    private ParticleSystem _speedLinesPS;

    private float _oldSpeed = -1;

    // Update is called once per frame
    void Update()
    {
        if (CourseMovementHandler.Instance.Speed == _oldSpeed) return;

        if (CourseMovementHandler.Instance.Speed <= 0)
        {
            _waterParticlesManager.StopParticleSystems();
            _speedLinesPS.Stop();
            return;
        }

        // If we were going slow before
        if (_oldSpeed <= 2 && CourseMovementHandler.Instance.Speed > 2)
        {
            _waterParticlesManager.StartParticleSystems();
        }

        // If we're moving fast enough for speed lines
        if (CourseMovementHandler.Instance.Speed >= 5)
        {
            _speedLinesPS.Play();
        } else
        {
            _speedLinesPS.Stop();
        }

        _oldSpeed = CourseMovementHandler.Instance.Speed;
    }
}
