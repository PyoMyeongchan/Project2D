using UnityEngine;

public class ItemBoxManager : MonoBehaviour
{
    private bool isClosed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isClosed && collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            { 
            
            }
        
        }
    }

}
