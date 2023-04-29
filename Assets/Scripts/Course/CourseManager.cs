using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public static CourseManager Instance;

    [SerializeField]
    private TubeSegmentSpawner _tubeSegmentSpawner;

    [SerializeField]
    private float _spawnInterval;

    [SerializeField]
    private Vector3 _movement;
    public Vector3 Movement { get => _movement; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        _spawnTimer = _spawnInterval;
    }

    private float _spawnTimer;
    private void Update()
    {
        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        _spawnTimer = _spawnInterval;

        _tubeSegmentSpawner.Spawn();
    }
}
