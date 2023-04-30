using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public static CourseManager Instance;

    [SerializeField]
    private TubeSegmentSpawner _tubeSegmentSpawner;
    [SerializeField]
    private MineSpawner _mineSpawner;

    [SerializeField]
    private float _segmentSpawnInterval;

    [SerializeField]
    private Vector3 _movement;
    public Vector3 Movement { get => _movement; }

    private float _spawnTimer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        _spawnTimer = _segmentSpawnInterval;
    }

    private void Update()
    {
        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        _spawnTimer = _segmentSpawnInterval;

        _tubeSegmentSpawner.Spawn();
        _mineSpawner.Spawn();
    }
}
