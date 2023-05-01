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
    private GameObject _pathFollowObj;

    [SerializeField]
    private List<CourseTubeSegment> _courseSegments;

    [SerializeField]
    [Range(1, 20)]
    private float _speed = 8;
    public float Speed { get => _speed; }

    private float _savedSpeedAtStart;

    [SerializeField]
    [Range(1, 20)]
    private float _rotationSpeed = 5;
    public float RotationSpeed { get => _rotationSpeed; }

    [SerializeField]
    private float _duration;

    private CourseTubeSegment _currentSegment;
    private int _currentIndex;
    private Vector3 _targetPoint;
    private Quaternion _targetRotation;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        if (_courseSegments == null)
        {
            _courseSegments = new List<CourseTubeSegment>();
        }
    }

    private void Start()
    {
        _currentIndex = 0;
        _savedSpeedAtStart = _speed;
        _speed = 0;
        SetupCurrentSegment();
    }

    public void StartMoving()
    {
        _speed = _savedSpeedAtStart;
    }

    private void Update()
    {
        if (_speed <= 0) return;

        if (_courseSegments.Count <= 0)
        {
            Debug.LogError("Coarse segments queue is empty!");
            return;
        }

        _targetPoint = _currentSegment.PathPoints[_currentIndex].position;

        Vector3 directionToTarget = _targetPoint - _playerContainer.transform.position;

        transform.position -= directionToTarget.normalized * _speed * Time.deltaTime;

        float dotProduct = Vector3.Dot(directionToTarget, _playerContainer.transform.forward);
        Debug.DrawRay(_playerContainer.transform.position + Vector3.up, -directionToTarget, Color.red, dotProduct);

        if (dotProduct <= 0)
        {
            // We have passed the current point, go to the next point
            _currentIndex++;

            // If we have no next point, go to the next segment
            if (_currentIndex > _currentSegment.PathPoints.Count - 1)
            {
                MoveToNextSegment();
            } else
            {
                _targetPoint = _currentSegment.PathPoints[_currentIndex].position;
            }

            Vector3 lookDir = _targetPoint - _playerContainer.transform.position;
            _targetRotation = Quaternion.LookRotation(lookDir);
        }

        // Smooth rotate player to look at target point
        _playerContainer.transform.rotation = Quaternion.Slerp(_playerContainer.transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }
    
    private void MoveToNextSegment()
    {
        // Remove old segment
        GameObject obj = _courseSegments[0].gameObject;
        Destroy(obj);
        _courseSegments.RemoveAt(0);

        CourseTubeSegment segmentAtEndOfList = _courseSegments[_courseSegments.Count - 1];

        // Spawn the next part of the course
        obj = _tubeSegmentSpawner.Spawn();
        obj.transform.position = segmentAtEndOfList.GetConnectionPoint().position;
        obj.transform.rotation = segmentAtEndOfList.GetConnectionPoint().rotation;
        _courseSegments.Add(obj.GetComponent<CourseTubeSegment>());

        SetupCurrentSegment();
    }

    private void SetupCurrentSegment()
    {
        // Get next segment
        _currentSegment = _courseSegments[0];

        // Skip 0 index since we are already there
        _currentIndex = 1;
        _targetPoint = _currentSegment.PathPoints[_currentIndex].position;
    }
}
