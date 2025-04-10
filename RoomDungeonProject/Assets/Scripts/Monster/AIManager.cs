using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class AIManager : MonoBehaviour
{
    public List<SpawnMonsterData> MonsterList = new List<SpawnMonsterData>();
    public float spawnRangeX = 10.0F;
    public float spawnRangeY = 5.0f;
    public Transform[] spawnPoints;
    private Queue<Transform> spawnQueue;



    public void SpawnAllMonsters()
    {
        spawnQueue = new Queue<Transform>(spawnPoints.OrderBy(x => Random.value));

        foreach (SpawnMonsterData info in MonsterList)
        {
            SpawnMonsterGroup(info);
        }
    }

    void SpawnMonsterGroup(SpawnMonsterData info)
    {
        for (int i = 0; i < info.count; i++)
        {
            Vector2 spawnPos = GetSpawnPosition();

            GameObject obj = Instantiate(info.monsterPrefab, spawnPos, Quaternion.identity);

            ParticleManager.Instance.ParticlePlay(ParticleType.MonsterSpawn, spawnPos, new Vector3(4f, 4f, 4f));
            SoundManager.instance.PlaySFX(SFXType.SpawnSound);

            // � Ÿ������ prefab�� ���� ��ũ��Ʈ�� �Ǵ�
            var rat = obj.GetComponent<RatManager>();
            if (rat != null)
            {
                rat.speed = Random.Range(info.minSpeed, info.maxSpeed);
                rat.hp = Random.Range(info.minHP, info.maxHP);
                rat.damage = Random.Range(info.minDamage, info.maxDamage);
                continue;
            }

            /* ���̷��� �߰��ϱ�
            var skeleton = obj.GetComponent<SkeletonManager>();
            if (skeleton != null)
            {
                skeleton.speed = Random.Range(info.minSpeed, info.maxSpeed);
                skeleton.hp = Random.Range(info.minHP, info.maxHP);
                skeleton.damage = Random.Range(info.minDamage, info.maxDamage);
                continue;
            }
            */
            Debug.LogWarning("���� �����տ� ��ũ��Ʈ�� �پ����� �ʰų� ���νĵ� Ÿ��.");
        }
    }

    Vector2 GetSpawnPosition()
    {
        if (spawnQueue != null && spawnPoints.Length > 0)
        {
            int index = Random.Range(0, spawnPoints.Length);
            return spawnPoints[index].position;
        }
        else
        {
            float x = Random.Range(-spawnRangeX, spawnRangeX);
            float y = Random.Range(-spawnRangeY, spawnRangeY);
            return new Vector2(x, y);
        }
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

[System.Serializable]
public class SpawnMonsterData
{
    public GameObject monsterPrefab;
    public int count;
    public float minSpeed = 1f;
    public float maxSpeed = 10f;
    public float minHP = 1f;
    public float maxHP = 10f;
    public float minDamage = 1f;
    public float maxDamage = 5f;

}
