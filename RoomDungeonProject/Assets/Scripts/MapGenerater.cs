using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    public Transform startPoint;
    public GameObject lastMapPrefab;
    public int mapCount = 5;      

    private Vector3 nextSpawnPosition;

    void Start()
    {
        List<GameObject> availableMaps = new List<GameObject>(mapPrefabs);
        nextSpawnPosition = startPoint.position;

        for (int i = 0; i < mapCount-1; i++)
        {

            int index = Random.Range(0, availableMaps.Count);
            GameObject selectedMap = availableMaps[index];
            availableMaps.RemoveAt(index); 

            SpawnMap(selectedMap);
        }

        SpawnMap(lastMapPrefab);
    }

    void SpawnMap(GameObject prefab)
    {
        GameObject mapPiece = Instantiate(prefab, nextSpawnPosition, Quaternion.identity);

        // 출구 위치를 찾아 다음 조각을 배치
        Transform exitPoint = mapPiece.transform.Find("ExitPoint");
        if (exitPoint != null)
        {
            nextSpawnPosition = exitPoint.position;
        }
    }
}
