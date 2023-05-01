using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseTubeSegment : MonoBehaviour
{
    [SerializeField]
    private Transform _nextConnectionPoint;
    public Transform NextConnectionPoint { get => _nextConnectionPoint; }

    [SerializeField]
    private List<Transform> _curveControlPoints;
    public List<Transform> CurveControlPoints { get => _curveControlPoints; }

    private BezierCurve bezierCurve;

    private float _gizmoSize = 0.25f;

    private void Awake()
    {
        bezierCurve = new BezierCurve(transform.position, _curveControlPoints[0].position, NextConnectionPoint.position);
    }

    public Vector3 GetPointOnCurveTime(float t)
    {
        return bezierCurve.GetPoint(t) + transform.position - bezierCurve.a;
    }

    public Vector3 GetPointOnCurve(int index)
    {
        return bezierCurve.Points[index] + transform.position - bezierCurve.a;
    }

    public int GetNumPointsOnCurve()
    {
        return bezierCurve.Points.Count;
    }

    private void OnDrawGizmos()
    {
        if (bezierCurve == null)
        {
            bezierCurve = new BezierCurve(transform.position, _curveControlPoints[0].position, NextConnectionPoint.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one * _gizmoSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_nextConnectionPoint.position, Vector3.one * _gizmoSize);

        Gizmos.color = Color.grey;
        Gizmos.DrawCube(_curveControlPoints[0].position, Vector3.one * _gizmoSize);

        Vector3 previousPoint = transform.position;
        for (int i = 0; i < bezierCurve.Points.Count; ++i)
        {
            Vector3 point = bezierCurve.Points[i] + transform.position - bezierCurve.a;

            Gizmos.color = Color.white;
            Gizmos.DrawLine(previousPoint, point);

            Gizmos.DrawSphere(point, _gizmoSize / 4);
            previousPoint = point;
        }
    }

    private void OnDrawGizmosSelected()
    {
        bezierCurve = new BezierCurve(transform.position, _curveControlPoints[0].position, NextConnectionPoint.position);
    }
}
