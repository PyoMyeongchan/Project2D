using System.Collections;
using UnityEngine;

// 몬스터의 사운드 맞는 피격 파티클 관리
public enum MonsterType
{ 
    None,
    Rat,
    Skeleton
}


public class MonsterManager : MonoBehaviour
{
    private Color originalColor;
    private Renderer objectRenderer;
    public float colorChangeDuration = 0.5f;
    private float monsterHP;
    public MonsterType monsterType = MonsterType.None;

    private bool isInvincible = false;


    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (!isInvincible)
            {
                StartCoroutine(IsInvincible());
                StartCoroutine(ChangeColor());
                StartCoroutine(CameraManager.instance.Shake());

                ParticleManager.Instance.ParticlePlay(ParticleType.PlayerAttack, transform.position, new Vector3(4f, 4f, 4f));
                
            }
        }
    }


    IEnumerator ChangeColor()
    {
        // 몬스터마다 소리 다르게 만들기
        if (monsterType == MonsterType.Rat)
        {
            SoundManager.instance.PlaySFX(SFXType.MouseHitSound);
        }
        else if (monsterType == MonsterType.Skeleton)
        { 
                    
        }
        

        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(colorChangeDuration);
        objectRenderer.material.color = originalColor;
    }

    IEnumerator IsInvincible()
    {
        isInvincible = true;

        yield return new WaitForSeconds(0.3f);

        isInvincible = false;

    }

}
