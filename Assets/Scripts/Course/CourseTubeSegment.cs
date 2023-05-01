using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseTubeSegment : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _pathPoints;
    public List<Transform> PathPoints { get => _pathPoints; }

    private float _gizmoSize = 0.25f;

    private void OnDrawGizmos()
    {
        if (PathPoints.Count <= 0) return;
        
        Vector3 previousPoint = PathPoints[0].position;
        for (int i = 0; i < PathPoints.Count; ++i)
        {
            Vector3 point = PathPoints[i].position;

            Gizmos.color = Color.white;
            Gizmos.DrawLine(previousPoint, point);

            Gizmos.DrawSphere(point, _gizmoSize / 4);
            previousPoint = point;
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    bezierCurve = new BezierCurve(transform.position, _curveControlPoints[0].position, NextConnectionPoint.position);
    //}

    public Transform GetConnectionPoint()
    {
        return PathPoints[PathPoints.Count - 1];
    }
}
