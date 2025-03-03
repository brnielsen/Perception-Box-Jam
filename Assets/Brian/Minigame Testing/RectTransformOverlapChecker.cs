
using UnityEngine;

public static class RectTransformOverlapChecker
{

    public static bool IsOverLapping(RectTransform rectTransform1, RectTransform rectTransform2, float cushion)
    {
        Rect rect1 = GetWorldRect(rectTransform1, cushion);
        Rect rect2 = GetWorldRect(rectTransform2, cushion);

        return rect1.Overlaps(rect2);
    }

    public static Rect GetWorldRect(RectTransform rectTransform, float cushion)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float x = corners[0].x + cushion;
        float y = corners[0].y + cushion;
        float width = (corners[2].x - corners[0].x) - (2 * cushion);
        float height = (corners[2].y - corners[0].y) - (2 * cushion);

        width = Mathf.Max(0, width);
        height = Mathf.Max(0, height);

        return new Rect(x, y, width, height);
    }
}
