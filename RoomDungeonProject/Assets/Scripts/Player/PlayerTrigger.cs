using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject keyUI;
    public GameObject moveTutorial;
    public GameObject JumpTutorial;
    public GameObject AttackTutorial;
    public GameObject RunTutorial;

    private Vector2 startPlayerPos;
    public Transform startMapPos;

    public Camera mainCamera;
    public CinemachineCamera mainCam;
    public Collider2D startCollide;

    private PlayerDamage playerDamage;
    private PlayerAnimation playerAnimation;


    private void Start()
    {
        startPlayerPos = transform.position;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerDamage = GetComponent<PlayerDamage>();

    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 함정에 있을때 피격유지
        if (collision.gameObject.layer == 13)
        {
            if (!playerDamage.isInvincible)
            {
                SoundManager.instance.PlaySFX(SFXType.Damaged);
                playerAnimation.TriggerDamaged();
                StartCoroutine(playerDamage.IsInvincible());
                StartCoroutine(CameraManager.instance.DamagedShake());
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            keyUI.SetActive(true);
            GameManager.instance.AddKey(1);
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("DeadZone"))
        { 
            gameObject.transform.position = startPlayerPos;
        }

        if (collision.CompareTag("Tutorial1"))
        {
            moveTutorial.SetActive(true);
        }

        if (collision.CompareTag("Tutorial2"))
        {
            JumpTutorial.SetActive(true);
        }

        if (collision.CompareTag("Tutorial3"))
        {
            AttackTutorial.SetActive(true);

        }

        if (collision.CompareTag("Tutorial4"))
        {
            RunTutorial.SetActive(true);

        }

        // F키 누르면 안되는지 확인해보기
        if (collision.CompareTag("Door"))
        {
            StartCoroutine(ChangFirstMap());
            
        }

        
        if (collision.gameObject.layer == 11)
        {

            PolygonCollider2D newCollider = collision.GetComponentInChildren<PolygonCollider2D>();

            if (newCollider != null)
            {
                StartCoroutine(ChangInMap(newCollider));
            }
             
        }

        // 몬스터 공격시 피격
        if (collision.gameObject.layer == 12)
        {
            if (!playerDamage.isInvincible)
            {
                SoundManager.instance.PlaySFX(SFXType.Damaged);
                StartCoroutine(CameraManager.instance.Shake());
                playerAnimation.TriggerDamaged();
                StartCoroutine(playerDamage.IsInvincible());
            }
        }
 
    }

    IEnumerator ChangInMap(PolygonCollider2D newCollider)
    {
        if (FadeSystemManager.instance.isFading == false)
        { 
            yield return FadeSystemManager.instance.FadeOut();
        }
        var brain = mainCamera.GetComponent<CinemachineBrain>();
        brain.enabled = false;

        var confiner = mainCam.GetComponent<CinemachineConfiner2D>();

        confiner.BoundingShape2D = null;
        confiner.InvalidateBoundingShapeCache();
        mainCam.Target.TrackingTarget = null;   

        yield return new WaitForSeconds(0.1f);

        mainCam.Target.TrackingTarget = gameObject.transform;
        confiner.BoundingShape2D = newCollider;
        confiner.InvalidateBoundingShapeCache();
        
        AdjustCameraZoomToBounds(newCollider.bounds);

        brain.enabled = true;        
        yield return FadeSystemManager.instance.FadeIn();
    }
    
    IEnumerator ChangFirstMap()
    {

        yield return FadeSystemManager.instance.FadeOut();
        transform.position = startMapPos.position;
        StartCoroutine(TimeStop(2.0f));
        yield return new WaitForSeconds(1.0f);
         
    }

    // Realtime으로하여야 실제 시간이 지나감
    IEnumerator TimeStop(float time)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }

    void AdjustCameraZoomToBounds(Bounds bounds)
    {
        float screenAspect = (float)Screen.width / Screen.height;

        float targetHeight = bounds.size.y / 2f;
        float targetWidth = bounds.size.x / 2f / screenAspect;

        float targetSize = Mathf.Min(targetHeight, targetWidth);
        targetSize = Mathf.Clamp(targetSize, 3f, 10f);

        var lens = mainCam.Lens;
        lens.OrthographicSize = targetSize;
        mainCam.Lens = lens;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tutorial1"))
        {
            moveTutorial.SetActive(false);
        }

        if (collision.CompareTag("Tutorial2"))
        {
            JumpTutorial.SetActive(false);

        }

        if (collision.CompareTag("Tutorial3"))
        {
            AttackTutorial.SetActive(false);

        }

        if (collision.CompareTag("Tutorial4"))
        {
            RunTutorial.SetActive(false);

        }

    }

}
