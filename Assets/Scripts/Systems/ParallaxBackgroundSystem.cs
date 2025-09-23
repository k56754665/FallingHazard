using UnityEngine;

public class ParallaxBackgroundSystem : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayerData
    {
        public Texture2D texture;           // 스크롤할 알파 포함 텍스처
        public Vector2 speed = Vector2.up;  // 스크롤 속도 (방향+배율)
        public Color color = Color.white;   // 틴트 색
    }

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int Offset  = Shader.PropertyToID("_Offset");

    [Header("레이어 설정")]
    [SerializeField] private ParallaxLayerData[] layers;

    [Header("공통 ShaderGraph 머티리얼")]
    [SerializeField] private Material baseMaterial;

    [Header("SpriteRenderer에 넣을 기본 흰색 스프라이트")]
    [SerializeField] private Sprite whiteSprite;

    private SpriteRenderer[] _renderers;
    private MaterialPropertyBlock[] _mpbs;
    private Vector2[] _offsets;

    private void Awake()
    {
        _renderers = new SpriteRenderer[layers.Length];
        _mpbs      = new MaterialPropertyBlock[layers.Length];
        _offsets   = new Vector2[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            _mpbs[i] = new MaterialPropertyBlock();

            // 자식 오브젝트 생성
            GameObject layerObj = new GameObject($"ParallaxLayer_{i}");
            layerObj.transform.SetParent(transform);
            layerObj.transform.localScale = Vector3.one;

            // SpriteRenderer 추가
            var sr = layerObj.AddComponent<SpriteRenderer>();
            sr.sprite          = whiteSprite;
            sr.sharedMaterial  = baseMaterial;
            sr.sortingOrder    = i;
            sr.sortingLayerName = "Background";
            sr.color           = layers[i].color;

            _renderers[i] = sr;

            // 텍스처와 초기값
            ApplyLayerData(i);
        }
    }

    private void Update()
    {
        float globalSpeed = SpeedSystem.Speed;

        for (int i = 0; i < layers.Length; i++)
        {
            // 누적 오프셋 (벡터 방향+배율 반영)
            _offsets[i] += layers[i].speed * (globalSpeed * Time.deltaTime);

            var mpb = _mpbs[i];
            mpb.SetVector(Offset, _offsets[i]);
            _renderers[i].SetPropertyBlock(mpb);
        }
    }

    private void ApplyLayerData(int index)
    {
        ParallaxLayerData data = layers[index];
        SpriteRenderer sr = _renderers[index];
        MaterialPropertyBlock mpb = _mpbs[index];

        mpb.SetTexture(MainTex, data.texture);
        mpb.SetVector(Offset, Vector2.zero);
        sr.SetPropertyBlock(mpb);
    }
}
