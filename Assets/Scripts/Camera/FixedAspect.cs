using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspect : MonoBehaviour
{
    [SerializeField] private float targetAspectWidth = 9f;
    [SerializeField] private float targetAspectHeight = 16f;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.backgroundColor = Color.black;               // 여백은 검은색
        cam.clearFlags = CameraClearFlags.SolidColor;

        float targetAspect = targetAspectWidth / targetAspectHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        // 스케일 비율 계산
        float scale = windowAspect / targetAspect;

        if (scale > 1f)
        {
            // 윈도우가 더 넓음 → 좌우에 pillarbox
            float normalizedWidth = 1f / scale;
            float barThickness = (1f - normalizedWidth) / 2f;

            cam.rect = new Rect(barThickness, 0f, normalizedWidth, 1f);
        }
        else
        {
            // 윈도우가 더 좁음 → 위아래 letterbox
            float normalizedHeight = scale;
            float barThickness = (1f - normalizedHeight) / 2f;

            cam.rect = new Rect(0f, barThickness, 1f, normalizedHeight);
        }
    }
}