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

        // FŰ ������ �ȵǴ��� Ȯ���غ���
        if (collision.CompareTag("Door"))
        {           
            StartCoroutine(ChangFirstMap());
        }

        // �Ҵ��� �ǰ��ִµ� �ݶ��̴��� ũ�� �������� ī�޶� �̵��� ���� ����
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
    // �����Ҷ� �ι� ���̵��� ���̵�ƿ��� �Ǵ� ����
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

    // Realtime�����Ͽ��� ���� �ð��� ������
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
