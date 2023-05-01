using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSegmentSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _plainSegments;
    [SerializeField]
    private List<GameObject> _hostileSegments;
    [SerializeField]
    private List<GameObject> _turnSegments;
    [SerializeField]
    private GameObject _gateSegment;

    public GameObject SpawnRandomTurn()
    {
        return Instantiate(_turnSegments[Random.Range(0, _turnSegments.Count)], transform);
    }

    public GameObject SpawnRandomPlainSegment()
    {
        return Instantiate(_plainSegments[Random.Range(0, _plainSegments.Count)], transform);
    }

    public GameObject SpawnRandomHostileSegment()
    {
        return Instantiate(_hostileSegments[Random.Range(0, _hostileSegments.Count)], transform);
    }

    public GameObject SpawnGate()
    {
        return Instantiate(_gateSegment, transform);
    }
}
