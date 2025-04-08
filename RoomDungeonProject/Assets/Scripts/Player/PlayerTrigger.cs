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


    private void Start()
    {
        startPlayerPos = transform.position;

    }

    private void Update()
    {
        
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

        // 할당은 되고있는데 콜라이더의 크기 문제인지 카메라가 이동을 하지 않음
        if (collision.gameObject.layer == 11)
        {

            PolygonCollider2D newCollider = collision.GetComponentInChildren<PolygonCollider2D>();

            if (newCollider != null)
            {
                StartCoroutine(ChangInMap(newCollider));
            }
             

        }
 
    }

    IEnumerator ChangInMap(PolygonCollider2D newCollider)
    {
        yield return FadeSystemManager.instance.FadeOut();

        var brain = mainCamera.GetComponent<CinemachineBrain>();
        brain.enabled = false;

        var confiner = mainCam.GetComponent<CinemachineConfiner2D>();

        confiner.BoundingShape2D = null;
        confiner.InvalidateBoundingShapeCache();

        yield return null; 

        confiner.BoundingShape2D = newCollider;
        confiner.InvalidateBoundingShapeCache();
        AdjustCameraZoomToBounds(newCollider.bounds);

        brain.enabled = true;
        yield return FadeSystemManager.instance.FadeIn();
    }
    // 시작할때 두번 페이드인 페이드아웃이 되는 현상
    IEnumerator ChangFirstMap()
    {
        yield return FadeSystemManager.instance.FadeOut();
        
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;

        var confiner = mainCam.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = null;
        transform.position = startMapPos.position;   
        confiner.InvalidateBoundingShapeCache();
        

        mainCamera.GetComponent<CinemachineBrain>().enabled = true;
        
        StartCoroutine(TimeStop());
        
        yield return new WaitForSeconds(1.0f);        
        yield return FadeSystemManager.instance.FadeIn();
        
    }

    // Realtime으로하여야 실제 시간이 지나감
    IEnumerator TimeStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2.0f);
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
