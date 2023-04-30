using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public static CourseManager Instance;

    [SerializeField]
    private TubeSegmentSpawner _tubeSegmentSpawner;

    [SerializeField]
    private GameObject _playerContainer;

    [SerializeField]
    [Range(1, 20)]
    private float _speed = 5;
    public float Speed { get => _speed; }

    private float _spawnTimer;
    private List<Vector3> _coursePoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        _spawnTimer = CalculateTimer();
    }

    private void Update()
    {
        if (_speed == 0)
        {
            return;
        }

        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        _spawnTimer = CalculateTimer();

        switch (Random.Range(0, 1))
        {
            default: _tubeSegmentSpawner.Spawn(); break;
        }
    }

    // Since the tube segment is 10 units long,
    // we need 10 / speed to get the time it takes to move the whole length
    private float CalculateTimer()
    {
        if (_speed == 0) return 0;

        return 10 / _speed;
    }
}
