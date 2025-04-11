using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapGateManager : MonoBehaviour
{
    public GameObject mapGate;

    public AIManager aIManager;

    private void Update()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Monster");

        if (gameObjects.Length == 0)
        {
            mapGate.SetActive(false);
            // ������Ʈ�� �Ҹ��� ������ ����
            //SoundManager.instance.PlaySFX(SFXType.GateOpenSound);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapGate.SetActive(true);
        }

    }




}
