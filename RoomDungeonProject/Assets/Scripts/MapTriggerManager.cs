using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class MapTriggerManager : MonoBehaviour
{
    public float pushDistance = 1f;
    public float stopDuration = 0.2f;
    private PlayerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove = collision.GetComponent<PlayerMove>();

            if (playerMove != null)
            {
                StartCoroutine(PushAndStopPlayer(collision.transform));
            }
        }
    }

    IEnumerator PushAndStopPlayer(Transform player)
    {
        Vector3 triggerPos = transform.position;
        Vector3 playerPos = player.position;
        Vector3 offsetDir = playerPos.x < triggerPos.x ? Vector3.right : Vector3.left;
        player.position += offsetDir * pushDistance;

        float originalSpeed = playerMove.moveSpeed;
        playerMove.moveSpeed = 0f;

        yield return new WaitForSeconds(stopDuration);

        playerMove.moveSpeed = originalSpeed;
    }
}
