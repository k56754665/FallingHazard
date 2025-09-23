using UnityEngine;

public class SpeedSystem : MonoBehaviour
{
    public static float Speed { get; private set; }
    public static float MaxSpeed { get; private set; }

    [Header("속도 설정")]
    [SerializeField] private float diveSpeed;
    [SerializeField] private float glideSpeed;

    [Header("애니메이션 커브")]
    [SerializeField] private AnimationCurve diveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve glideCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("전환 시간")]
    [SerializeField] private float diveDuration;
    [SerializeField] private float glideDuration;

    private float _startSpeed;
    private float _targetSpeed;
    private float _elapsed;
    private float _duration;
    private AnimationCurve _currentCurve;

    private static SpeedSystem _instance;

    private void Awake()
    {
        _instance = this;
        _startSpeed = diveSpeed;
        _targetSpeed = diveSpeed;
        Speed = 0;
        MaxSpeed = diveSpeed;
    }

    private void Update()
    {
        if (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsed / _duration);

            // 커브 평가값 (0~1)을 사용해서 속도 보간
            float curveValue = _currentCurve.Evaluate(t);
            Speed = Mathf.LerpUnclamped(_startSpeed, _targetSpeed, curveValue);
        }
        else
        {
            Speed = _targetSpeed; // 보간 종료
        }
    }

    public static void SetDive(bool isDive)
    {
        if (_instance == null) return;

        _instance._startSpeed = Speed;
        if (isDive)
        {
            _instance._targetSpeed = _instance.diveSpeed;
            _instance._duration = _instance.diveDuration;
            _instance._currentCurve = _instance.diveCurve;
        }
        else
        {
            _instance._targetSpeed = _instance.glideSpeed;
            _instance._duration = _instance.glideDuration;
            _instance._currentCurve = _instance.glideCurve;
        }

        _instance._elapsed = 0f;
    }

    private void OnEnable() => _instance = this;
    private void OnDisable() { if (_instance == this) _instance = null; }
}
