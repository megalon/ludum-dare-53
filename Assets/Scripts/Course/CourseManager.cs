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

    private Vector3 _startPos;

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

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, -Speed) * Time.deltaTime);

        if (_courseSegments.Count <= 0)
        {
            Debug.LogError("Coarse segments queue is empty!");
            return;
        }

        // Get next segment
        CourseTubeSegment currentSegment = _courseSegments[0];

        // Distance from pos to start of this node
        Debug.DrawLine(_playerContainer.transform.position, currentSegment.NextConnectionPoint.position, Color.red);

        Vector3 direction = _playerContainer.transform.position - currentSegment.NextConnectionPoint.position;
        float dot = Vector3.Dot(direction, currentSegment.NextConnectionPoint.forward);

        // If the connection point is behind us
        if (dot > 0)
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
        }
    }
}
