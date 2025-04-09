using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerOneWay : MonoBehaviour
{
    public GameObject OneWayPlatform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            if (OneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        { 
            OneWayPlatform = collision.gameObject;
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {
            OneWayPlatform = null;
        }
    }


    IEnumerator DisableCollision()
    {
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        BoxCollider2D boxMap = OneWayPlatform.GetComponent<BoxCollider2D>();
                
        Physics2D.IgnoreCollision(capsule, boxMap, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(capsule, boxMap, false);

    }

}
