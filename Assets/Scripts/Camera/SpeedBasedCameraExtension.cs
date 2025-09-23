using UnityEngine;
using Unity.Cinemachine;

[ExecuteAlways]
public class SpeedBasedCameraExtension : CinemachineExtension
{
    // [Header("Y Offset 설정")]
    // [SerializeField] private float bottomYOffset = -5f;   // 최고 속도일 때 카메라 위치
    // [SerializeField] private AnimationCurve offsetCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("카메라 쉐이킹 설정")]
    [SerializeField] private float shakeIntensity = 0.5f; // 최대 흔들림 강도
    [SerializeField] private float shakeSpeed = 5f;       // 흔들림 속도

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Body) return;

        float speed = SpeedSystem.Speed;
        float maxSpeed = Mathf.Max(0.01f, SpeedSystem.MaxSpeed); // 0 나누기 방지

        // 속도 → 0~1 비율로 정규화
        float t = Mathf.Clamp01(speed / maxSpeed);

        // === Y Offset (속도 비율 기반) ===
        // float curveValue = offsetCurve.Evaluate(t);
        // float yOffset = Mathf.Lerp(0f, bottomYOffset, curveValue);

        // === 쉐이킹 (속도 비율 기반) ===
        float noise = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2f - 1f);
        float shake = noise * shakeIntensity * t;

        // 최종 카메라 위치 보정
        state.PositionCorrection += new Vector3(shake, shake, 0);
    }
}