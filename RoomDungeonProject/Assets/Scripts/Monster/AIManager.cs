using Unity.VisualScripting;
using UnityEngine;


// 스포너를 관리
// 현재 스포너가 랜덤으로 나오는 현상이 있음 확인하고 수정해야함


public enum SpawnMonster
{ 
    None,
    Rat,
    Skeleton

}

public class AIManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRangeX = 10.0F;
    public float spawnRangeY = 5.0f;
    public int enemyCount = 5;
    public Transform[] spawnPoints;
    public SpawnMonster monsterType = SpawnMonster.None;

    private float monsterSpeed = 1.0f;
    private float monsterHP = 1.0f;
    private float monsterDamage = 5.0f;

    void Start()
    {
        SpawnEnemies();
        MonsterSetState();
    }

    private void Update()
    {
        
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Vector2 spawnPosition = spawnPoints[randomIndex].position;
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }
            else 
            { 
                float randomX = Random.Range(-spawnRangeX, spawnRangeY);
                float randomY = Random.Range(-spawnRangeY, spawnRangeX);
                Vector2 randomPosition = new Vector2 (randomX, randomY);
                Instantiate(monsterPrefab, randomPosition, Quaternion.identity);
            }
        }

    }

    void MonsterSetState()
    { 
        RatManager rat = monsterPrefab.GetComponent<RatManager>();
        float minSpeed = 1f;
        float maxSpeed = 10f;
        float minHP = 1f;
        float maxHP = 10f;
        float mindamage = 1f;
        float maxdamage = 5f;

        if (monsterType == SpawnMonster.Rat)
        {
            minSpeed = 1f;
            maxSpeed = 5f;
            minHP = 1f;
            maxHP = 10f;
            mindamage = 1f;
            maxdamage = 5f;
        }
        else if (monsterType == SpawnMonster.Skeleton)
        { 
        
        }

        monsterSpeed = Random.Range(minSpeed, maxSpeed);
        monsterDamage = Random.Range(mindamage, maxdamage);
        monsterHP = Random.Range(minHP, maxHP);
        rat.speed = monsterSpeed;
        rat.hp = monsterHP;
        rat.damage = monsterDamage;

    
    }

    private void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector2.zero, new Vector2(spawnRangeX * 2, spawnRangeY * 2));
        Gizmos.color = Color.blue;

        if (spawnPoints.Length > 0)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
            }
        }
    }

}
