using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool isInvincible = false;
    public float inevincibilityDuration = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }



    public IEnumerator IsInvincible()
    {
        isInvincible = true;
        Time.timeScale = 0.8f;
        float elapsedTime = 0f;
        float blinkInterval = 0.5f;

        Color originalColor = spriteRenderer.color;

        while (elapsedTime < inevincibilityDuration)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f);
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval * 2;
        }
        spriteRenderer.color = originalColor;
        isInvincible = false;
        Time.timeScale = 1.0f;
    }

}
