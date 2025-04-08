using System.Collections;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Color originalColor;
    private Renderer objectRenderer;
    public float colorChangeDuration = 0.5f;
    private float monsterHP;


    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("Ä®°ú Ãæµ¹Áß");
            StartCoroutine(ChangeColor());
            StartCoroutine(CameraManager.instance.Shake());

            ParticleManager.Instance.ParticlePlay(ParticleType.PlayerAttack, transform.position, new Vector3(4f, 4f, 4f));
        }
    }


    IEnumerator ChangeColor()
    { 
        SoundManager.instance.PlaySFX(SFXType.MouseHitSound);
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(colorChangeDuration);
        objectRenderer.material.color = originalColor;
    }

}
