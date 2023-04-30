using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Quadratic Bezier curve
    public static Vector3 QuadraticPoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public static Vector3 CubicPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 p0 = QuadraticPoint(a, b, c, t);
        Vector3 p1 = QuadraticPoint(b, c, d, t);
        return Vector3.Lerp(p0, p1, t);
    }
}
