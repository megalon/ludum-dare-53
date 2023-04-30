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

    private float _gizmoSize = 0.25f;
    private float _lineResolution = 0.05f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one * _gizmoSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_nextConnectionPoint.position, Vector3.one * _gizmoSize);

        Gizmos.color = Color.grey;
        Gizmos.DrawCube(_curveControlPoints[0].position, Vector3.one * _gizmoSize);

        DrawQuadraticCurve(transform.position, _curveControlPoints[0].position, _nextConnectionPoint.position);
    }

    private void DrawQuadraticCurve(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 prevPoint = a;
        for (float i = 0; i < 1; i += _lineResolution)
        {
            Gizmos.color = Color.white;
            Vector3 bezierPoint = Utils.QuadraticPoint(a, b, c, i);
            Gizmos.DrawLine(prevPoint, bezierPoint);
            prevPoint = bezierPoint;
        }
    }

    private Vector3 DrawCubicCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        Vector3 prevPoint = a;
        Debug.Log(b);
        for (float i = 0; i < 1; i += 0.025f)
        {
            Gizmos.color = Color.red;
            Vector3 bezierPoint = Utils.CubicPoint(a, b, c, d, i);
            Gizmos.DrawLine(prevPoint, bezierPoint);
            prevPoint = bezierPoint;
        }

        return Utils.CubicPoint(a, b, c, d, 1);
    }
}
