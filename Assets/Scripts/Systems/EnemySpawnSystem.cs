using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField] private RisingEnemy risingEnemyPrefab;
    [SerializeField] private FallingEnemy fallingEnemyPrefab;

    [Header("스폰 설정")]
    [SerializeField] private Vector2 spawnIntervalRange = new(1.25f, 2.5f);

    [Header("스폰 범위 (월드 좌표)")]
    [SerializeField] private Vector2 xRange = new(-5f, 5f);
    [SerializeField] private Vector2 yRangeRising = new(-6f, -4f);
    [SerializeField] private Vector2 yRangeFalling = new(4f, 6f);

    private float _spawnTimer;

    private void OnEnable()
    {
        ScheduleNextSpawn();
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            ScheduleNextSpawn();
        }
    }

    private void SpawnEnemy()
    {
        bool spawnRising = Random.value > 0.5f; // 50% 확률로 위/아래 선택

        float spawnX = Random.Range(xRange.x, xRange.y);
        float spawnY;

        if (spawnRising)
        {
            spawnY = Random.Range(yRangeRising.x, yRangeRising.y);
            Instantiate(risingEnemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity, transform);
        }
        else
        {
            spawnY = Random.Range(yRangeFalling.x, yRangeFalling.y);
            Instantiate(fallingEnemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity, transform);
        }
    }

    private void ScheduleNextSpawn()
    {
        float min = Mathf.Min(spawnIntervalRange.x, spawnIntervalRange.y);
        float max = Mathf.Max(spawnIntervalRange.x, spawnIntervalRange.y);
        _spawnTimer = Random.Range(min, max);
    }
}