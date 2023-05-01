using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseGenerator : MonoBehaviour
{
    public static CourseGenerator Instance;

    [SerializeField]
    private List<CourseTubeSegment> _courseSegments;

    [SerializeField]
    private TubeSegmentSpawner _tubeSegmentSpawner;

    private int _segmentsCreated;

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

        _segmentsCreated = 0;
    }

    public CourseTubeSegment GetCurrentSegment()
    {
        return _courseSegments[0];
    }

    public void MoveToNextSegment()
    {
        // Remove old segment
        GameObject obj = _courseSegments[0].gameObject;
        Destroy(obj);
        _courseSegments.RemoveAt(0);

        CourseTubeSegment segmentAtEndOfList = _courseSegments[_courseSegments.Count - 1];

        // Spawn the next part of the course
        obj = GenerateNextSegment();
        obj.transform.position = segmentAtEndOfList.GetConnectionPoint().position;
        obj.transform.rotation = segmentAtEndOfList.GetConnectionPoint().rotation;
        _courseSegments.Add(obj.GetComponent<CourseTubeSegment>());
        _segmentsCreated++;
    }

    private GameObject GenerateNextSegment()
    {
        if (_segmentsCreated % 10 == 0)
        {
            return _tubeSegmentSpawner.SpawnGate();
        }
        
        if (_segmentsCreated % 3 == 0)
        {
            return _tubeSegmentSpawner.SpawnRandomTurn();
        }

        if (Random.Range(0, 5) == 0)
        {
            return _tubeSegmentSpawner.SpawnRandomPlainSegment();
        }
        
        return _tubeSegmentSpawner.SpawnRandomHostileSegment();
    }
}
