using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseMovementHandler : MonoBehaviour
{
    public static CourseMovementHandler Instance;

    [SerializeField]
    private GameObject _playerContainer;

    [SerializeField]
    private GameObject _pathFollowObj;

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
    private Material _waterMaterial;

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
    }

    private void Start()
    {
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

        if (_currentSegment == null)
        {
            Debug.LogError("Current segment is null!");
            return;
        }

        _targetPoint = _currentSegment.PathPoints[_currentIndex].position;

        Vector3 directionToTarget = _targetPoint - _playerContainer.transform.position;

        // Move the transform
        transform.position -= directionToTarget.normalized * _speed * Time.deltaTime;

        // Update the water offset
        _waterMaterial.SetVector("_Offset", new Vector2(transform.position.x, transform.position.z));

        // Use the dot product to determine if target is in front of us, or we have passed it
        float dotProduct = Vector3.Dot(directionToTarget, _playerContainer.transform.forward);

        if (dotProduct <= 0)
        {
            // We have passed the current point, go to the next point
            _currentIndex++;

            // If we have no next point, go to the next segment
            if (_currentIndex > _currentSegment.PathPoints.Count - 1)
            {
                CourseGenerator.Instance.MoveToNextSegment();
                SetupCurrentSegment();
            }

            _targetPoint = _currentSegment.PathPoints[_currentIndex].position;

            Vector3 lookDir = _targetPoint - _playerContainer.transform.position;
            _targetRotation = Quaternion.LookRotation(lookDir);
        }

        // Smooth rotate player to look at target point
        _playerContainer.transform.rotation = Quaternion.Slerp(_playerContainer.transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void SetupCurrentSegment()
    {
        _currentSegment = CourseGenerator.Instance.GetCurrentSegment();

        // Skip point at index 0 since we are already there
        _currentIndex = 1;
        _targetPoint = _currentSegment.PathPoints[_currentIndex].position;
    }
}
