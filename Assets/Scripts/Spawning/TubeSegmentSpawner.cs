using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSegmentSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _segments;

    public void Spawn()
    {
        Instantiate(_segments[Random.Range(0, _segments.Count)], transform);
    }
}
