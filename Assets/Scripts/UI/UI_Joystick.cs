using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour
{
    [SerializeField] private Image joystickBase;
    [SerializeField] private Image joystickHandle;
    
    private Canvas _canvas;
    private Camera _cam;
    private float _joystickRadius;
    private Vector2 _startPosition;

    public Vector2 Direction { get; private set; }
    public float Magnitude { get; private set; }
    public bool IsTouching { get; private set; }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _cam = _canvas.worldCamera;
        _joystickRadius = joystickBase.rectTransform.sizeDelta.x / 2f;
        joystickBase.gameObject.SetActive(false);
    }

    /// <summary>
    /// Press 액션에서 호출: started/canceled만 구분
    /// </summary>
    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // 시작 위치는 Point에서 가장 최근 값으로 받아오기
            Vector2 screenPos = Mouse.current != null
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current.primaryTouch.position.ReadValue();

            // 현재 카메라 rect 안에 있는지 확인 (0~1 정규화 좌표)
            Vector2 viewportPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
            if (!_cam.rect.Contains(viewportPos))
            {
                Debug.Log("Press ignored: outside camera rect");
                return;
            }

            // 스크린 좌표 → 캔버스 로컬 좌표 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                screenPos,
                _cam,
                out Vector2 localPoint
            );

            joystickBase.rectTransform.anchoredPosition = localPoint;
            _startPosition = localPoint;

            joystickBase.gameObject.SetActive(true);
            IsTouching = true;

            Debug.Log($"[UI_Joystick] Press START at Screen:{screenPos}, Local:{localPoint}");
        }
        else if (context.canceled)
        {
            ResetJoystick();
        }
    }

    /// <summary>
    /// Point 액션에서 호출: 터치/마우스 위치가 움직일 때마다 갱신
    /// </summary>
    public void OnPosition(InputAction.CallbackContext context)
    {
        if (!IsTouching) return; // Press 중일 때만 처리

        Vector2 screenPos = context.ReadValue<Vector2>();
        
        // 스크린 좌표 → 캔버스 로컬 좌표 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            screenPos,
            _cam,
            out Vector2 localPoint
        );

        Vector2 localDelta = localPoint - _startPosition;
        float magnitude = localDelta.magnitude;

        Direction = localDelta.normalized;
        Magnitude = Mathf.Min(magnitude / _joystickRadius, 1f);

        if (magnitude > _joystickRadius)
            localDelta = Direction * _joystickRadius;

        joystickHandle.rectTransform.localPosition = localDelta;

        Debug.Log($"[UI_Joystick] Point PERFORMED | Screen:{screenPos}, Local:{localPoint}, Dir:{Direction}, Mag:{Magnitude}");
    }

    private void ResetJoystick()
    {
        joystickBase.gameObject.SetActive(false);
        joystickHandle.rectTransform.localPosition = Vector2.zero;
        Direction = Vector2.zero;
        Magnitude = 0f;
        IsTouching = false;

        Debug.Log("[UI_Joystick] Joystick Reset");
    }
}
