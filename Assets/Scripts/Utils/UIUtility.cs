using UnityEngine;

public static class UIUtility
{
    public class UISpace
    {
        public Canvas canvas;
        public Camera camera;
    }
    
    /// <summary>
    /// 스크린 좌표를 캔버스 로컬 좌표로 변환
    /// </summary>
    public static Vector2 ScreenToLocal(Vector2 screenPos, UISpace uiSpace)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiSpace.canvas.transform as RectTransform,
            screenPos,
            uiSpace.camera,
            out Vector2 localPoint
        );
        return localPoint;
    }
}
