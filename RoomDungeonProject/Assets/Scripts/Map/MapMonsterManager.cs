using UnityEngine;

public class MapMonsterManager : MonoBehaviour
{
    public AIManager spawner;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            hasSpawned = true;
            spawner.SpawnAllMonsters();
        }
    }

}
