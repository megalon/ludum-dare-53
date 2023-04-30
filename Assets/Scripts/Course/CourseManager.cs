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
    private List<CourseTubeSegment> _courseSegments;

    [SerializeField]
    [Range(1, 20)]
    private float _speed = 5;
    public float Speed { get => _speed; }

    private CourseTubeSegment _currentSegment;
    private float t;

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
        t = 0;
        SetupCurrentSegment();
    }

    private void Update()
    {
        if (_courseSegments.Count <= 0)
        {
            Debug.LogError("Coarse segments queue is empty!");
            return;
        }

        Vector3 direction = _playerContainer.transform.position - _currentSegment.NextConnectionPoint.position;

        float dot = Vector3.Dot(direction, _currentSegment.NextConnectionPoint.forward);

        Debug.DrawRay(_playerContainer.transform.position + Vector3.up, -direction, Color.red, dot);

        // If the connection point is behind us
        if (dot >= 0)
        {
            GameObject obj = _courseSegments[0].gameObject;
            Destroy(obj);
            _courseSegments.RemoveAt(0);

            CourseTubeSegment segmentAtEndOfList = _courseSegments[_courseSegments.Count - 1];

            // Spawn the next part of the course
            obj = _tubeSegmentSpawner.Spawn();
            obj.transform.position = segmentAtEndOfList.NextConnectionPoint.position;
            obj.transform.rotation = segmentAtEndOfList.NextConnectionPoint.rotation;
            _courseSegments.Add(obj.GetComponent<CourseTubeSegment>());

            SetupCurrentSegment();
        }

        t += Speed * Time.deltaTime;

        // Move the course along the path
        transform.position -= Utils.QuadraticPoint(_currentSegment.transform.position, _currentSegment.CurveControlPoints[0].position, _currentSegment.NextConnectionPoint.position, t);

        // Get the tangent of the point along the path
        Vector3 tangent = Utils.QuadraticTangent(_currentSegment.transform.position, _currentSegment.CurveControlPoints[0].position, _currentSegment.NextConnectionPoint.position, t);
        Debug.DrawRay(_playerContainer.transform.position, tangent, Color.cyan);

        // Rotate the player container to face the correct direction on the path
        _playerContainer.transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
    }

    private void SetupCurrentSegment()
    {
        // Get next segment
        _currentSegment = _courseSegments[0];
        t = 0;
    }
}
