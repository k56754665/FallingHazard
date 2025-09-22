using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayerData
    {
        public Texture2D texture;   // 스크롤할 알파 포함 텍스처
        public float speed = 1f;    // 스크롤 속도
        public Color color =  Color.white; // 틴트 색
    }
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int Speed = Shader.PropertyToID("_Speed");

    [Header("레이어 설정")]
    [SerializeField] private ParallaxLayerData[] layers;

    [Header("공통 ShaderGraph 머티리얼")]
    [SerializeField] private Material baseMaterial;

    [Header("SpriteRenderer에 넣을 기본 흰색 스프라이트")]
    [SerializeField] private Sprite whiteSprite;

    private SpriteRenderer[] _renderers;
    private MaterialPropertyBlock _mpb;

    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
        _renderers = new SpriteRenderer[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            // 자식 오브젝트 생성
            GameObject layerObj = new GameObject($"ParallaxLayer_{i}");
            layerObj.transform.SetParent(transform);
            layerObj.transform.localScale = new Vector3(10, 10, 1);

            // SpriteRenderer 추가
            var sr = layerObj.AddComponent<SpriteRenderer>();
            sr.sprite = whiteSprite;              // 흰색 사각형 Sprite
            sr.sharedMaterial = baseMaterial;     // 공통 ShaderGraph 머티리얼
            sr.sortingOrder = i;                  // 순서대로 위에 오도록
            sr.sortingLayerName = "Background";   // 정렬 레이어
            sr.color = layers[i].color;           // 틴트 색 적용

            _renderers[i] = sr;

            // 텍스처와 속도 초기 적용
            ApplyLayerData(i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            _renderers[i].GetPropertyBlock(_mpb);
            _mpb.SetFloat(Speed, layers[i].speed * GameManager.Instance.CurrentSpeed);
            _renderers[i].SetPropertyBlock(_mpb);
        }
    }

    private void ApplyLayerData(int index)
    {
        var data = layers[index];
        var sr = _renderers[index];

        sr.GetPropertyBlock(_mpb);
        _mpb.SetTexture(MainTex, data.texture);
        _mpb.SetFloat(Speed, data.speed);
        sr.SetPropertyBlock(_mpb);
    }
}
