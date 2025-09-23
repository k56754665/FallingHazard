using UnityEngine;
using UnityEngine.UI;
using static UIUtility;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Image joystickBase;
    [SerializeField] private Image joystickHandle;
    
    private float _joystickRadius;
    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private UISpace _uiSpace = new();
    private InputManager _inputManager;

    public Vector2 Direction { get; private set; }
    public float Magnitude { get; private set; }
    public bool IsTouching { get; private set; }

    private void Awake()
    {
        _uiSpace.canvas = GetComponent<Canvas>();
        _uiSpace.camera = _uiSpace.canvas.worldCamera;
        _joystickRadius = joystickBase.rectTransform.sizeDelta.x / 2f;
        joystickBase.gameObject.SetActive(false);
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.OnStartPressEvent += HandlePress;
        _inputManager.OnCancelPressEvent += ResetJoystick;
        _inputManager.OnPositionEvent += HandlePosition;
    }

    private void OnDestroy()
    {
        if (_inputManager == null) return;
        _inputManager.OnStartPressEvent -= HandlePress;
        _inputManager.OnCancelPressEvent -= ResetJoystick;
        _inputManager.OnPositionEvent -= HandlePosition;
    }

    private void HandlePress(Vector2 screenPos)
    {
        // 캔버스 밖에서는 조이스틱이 눌리지 않음
        RectTransform canvasRect = _uiSpace.canvas.transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(canvasRect, screenPos, _uiSpace.camera))
            return;
        
        Vector2 localPoint = ScreenToLocal(screenPos, _uiSpace);

        joystickBase.rectTransform.anchoredPosition = localPoint;
        _startPosition = localPoint;
        joystickBase.gameObject.SetActive(true);
        IsTouching = true;
        
        GameManager.Instance.SetDive(true);
    }

    private void HandlePosition(Vector2 screenPos)
    {
        if (!IsTouching) return;

        Vector2 localPoint = ScreenToLocal(screenPos, _uiSpace);

        Vector2 localDelta = localPoint - _startPosition;
        float magnitude = localDelta.magnitude;

        Direction = localDelta.normalized;
        Magnitude = Mathf.Min(magnitude / _joystickRadius, 1f);

        if (magnitude > _joystickRadius)
            localDelta = Direction * _joystickRadius;

        joystickHandle.rectTransform.localPosition = localDelta;
    }

    private void ResetJoystick()
    {
        joystickBase.gameObject.SetActive(false);
        joystickHandle.rectTransform.localPosition = Vector2.zero;
        Direction = Vector2.zero;
        Magnitude = 0f;
        IsTouching = false;
        GameManager.Instance.SetDive(false);
    }
}
