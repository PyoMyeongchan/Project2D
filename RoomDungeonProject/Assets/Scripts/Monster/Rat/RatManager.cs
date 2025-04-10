using System.Collections;
using UnityEditor;
using UnityEngine;


public enum StateType
{ 
    None,
    Idle,
    Run,
    ChaseRun,
    Attack,
    Dead
}

// Rat에 대한 공격 죽음 넣기
public class RatManager : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 3.0f;
    private Vector2 startPos;
    private int direction = 1;

    public float hp = 5f;
    public float damage = 1.0f;

    public StateType currentState = StateType.Idle;

    // 플레이어보다 1칸식 뒤나 앞으로
    public Transform player;
    public float chaseRange = 5.0f;
    public float attackingRange = 1.5f;
    public bool isAttacking = false;
    private float stateChangeInterval = 3.0f;
    private Coroutine stateChange;

    private RatAnimaton ratAnimaton;

    public GameObject ratAttack1;
    public GameObject ratAttack2;


    void Start()
    {
        
        startPos = transform.position;
        int randomChoice = Random.Range(0, 1);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        stateChange = StartCoroutine(RandomStateChanger());

        ratAnimaton = GetComponent<RatAnimaton>();


    }

    private void Update()
    {
        if (currentState == StateType.None || player == null) return;

        float distancetoPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distancetoPlayer <= attackingRange && !isAttacking)
        {
            if (currentState != StateType.Attack)
            { 
                StopAllCoroutines();
                currentState = StateType.Attack;
                StartCoroutine(AttackRoutine());
            }
            return;
        }

        if (distancetoPlayer <= chaseRange)
        {
            
            if (currentState != StateType.Idle || currentState != StateType.Run)
            {
                if (stateChange != null)
                {
                    StopCoroutine(stateChange);
                }
                int chaseType = Random.Range(0, 2);
                currentState = chaseType == 0 ? StateType.Idle :StateType.Run;

                if (transform.position.x > player.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (transform.position.x <= player.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

            }
            
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float chaseSpeed = currentState == StateType.Run ? speed * 2 : speed;
            transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
            return;
                 
        }

        if ((currentState == StateType.Idle || currentState == StateType.Run) && distancetoPlayer > chaseRange)
        { 
            currentState = StateType.Idle;
            
            if (stateChange == null)
            {
                stateChange = StartCoroutine(RandomStateChanger());
            }
        }
        if (currentState == StateType.Attack)
        {
            return;
        }
        PatrolMovement();

    }


    private void PatrolMovement()
    {
        
        if (currentState == StateType.Idle)
        {
            
            if (transform.position.x > startPos.x + maxDistance)
            {
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (transform.position.x < startPos.x - maxDistance)
            {
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            float movespeed = currentState == StateType.Run ? speed * 2 : speed;

            
            transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);
        }



        
    }

    IEnumerator RandomStateChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(stateChangeInterval);
            int randomState = Random.Range(0, 3);
            currentState = (StateType)randomState;

        }
    }

    // 공격시 잠시 멈춤 구현하기
    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        
        yield return new WaitForSeconds(3.0f);
        ratAnimaton.RatAttack();
        isAttacking = false;
        
        stateChange = StartCoroutine(RandomStateChanger());
                
    }


    public void FlipAttackStart()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            ratAttack2.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            ratAttack1.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void FilpAttackEnd()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            ratAttack1.GetComponent<BoxCollider2D>().enabled = false;
            ratAttack2.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            ratAttack1.GetComponent<BoxCollider2D>().enabled = false;
            ratAttack2.GetComponent<BoxCollider2D>().enabled = false;
        }

    }


}
