using UnityEngine;

public static class BezierTool
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float coeficientT) {
        coeficientT = Mathf.Clamp01(coeficientT);
        var shiftFactor = 1f - coeficientT;
        var pointResult = shiftFactor * shiftFactor * shiftFactor * p0 +
                          3f * shiftFactor * shiftFactor * coeficientT * p1 +
                          3f * shiftFactor * coeficientT * coeficientT * p2 +
                          coeficientT * coeficientT * coeficientT * p3;
        return pointResult;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float coeficientT) {
        coeficientT = Mathf.Clamp01(coeficientT);
        float shiftFactor = 1f - coeficientT;
        var firstDerivativeResult =
            3f * shiftFactor * shiftFactor * (p1 - p0) +
            6f * shiftFactor * coeficientT * (p2 - p1) +
            3f * coeficientT * coeficientT * (p3 - p2);
        return firstDerivativeResult;
    }
}
