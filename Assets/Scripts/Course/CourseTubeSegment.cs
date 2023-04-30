using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseTubeSegment : MonoBehaviour
{
    [SerializeField]
    private Transform _nextConnectionPoint;
    public Transform NextConnectionPoint { get => _nextConnectionPoint; }
}
