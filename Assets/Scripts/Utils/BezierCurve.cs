using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    public Vector3 a, b, c;

    private List<Vector3> _points;
    public List<Vector3> Points { get => _points; }

    public BezierCurve(Vector3 a, Vector3 b, Vector3 c, int numSegments = 10)
    {
        this.a = a;
        this.b = b;
        this.c = c;

        _points = new List<Vector3>();

        for (int i = 0; i < numSegments; ++i)
        {
            float t = (float)i / numSegments;
            _points.Add(Utils.QuadraticPoint(a, b, c, t));
        }
    }

    public Vector3 GetPoint(float t)
    {
        if (t <= 0) return a;
        if (t >= 1) return b;
        return _points[Mathf.CeilToInt(_points.Count * t)];
    }
}

