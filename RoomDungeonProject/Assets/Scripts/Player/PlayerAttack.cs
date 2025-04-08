using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private Animator animator;
    private PlayerMove playerMove;

    public GameObject attackObject1;
    public GameObject attackObject2;




    public bool isAttacking = false;

    [Header("애니메이션 상태 이름")]
    public string attackStateName = "Attack1Ani";

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }


    public void InputAttack()
    {
        if (Input.GetButtonDown("Fire1") && playerMove.isRolling == false && !isAttacking)
        {
            PerformAttack(); 

            //Invoke("AirShake", 0.3f);
                                    
        }
    }

    void AirShake()
    {
        StartCoroutine(CameraManager.instance.AirShake());
    }






    public void PerformAttack()
    {
        if (isAttacking) 
        {
            return;
        }
        // 예외처리
        if (playerAnimation != null)
        {
            playerAnimation.TriggerAttack();
        }

        StartCoroutine(AttackCoolDownByAni());

    }

    // 공격 딜레이 넣기
    private IEnumerator AttackCoolDownByAni()
    { 
        isAttacking = true;        
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(attackStateName))
        {
            float animationLength = stateInfo.length;

            yield return new WaitForSeconds(animationLength);
            
        }
        else
        {            
            yield return new WaitForSeconds(0.5f);

        }        
        isAttacking = false;
        

    }

    // 공격 좌우 반전 코드
    public void FilpAttackStart()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {            
            attackObject2.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            attackObject1.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void FilpAttackEnd()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            attackObject1.GetComponent<BoxCollider2D>().enabled = false;
            attackObject2.GetComponent<BoxCollider2D>().enabled = false;            
        }
        else
        {
            attackObject1.GetComponent<BoxCollider2D>().enabled = false;
            attackObject2.GetComponent<BoxCollider2D>().enabled = false;
        }

    }


    public void AttackSound()
    {
        SoundManager.instance.PlaySFX(SFXType.Attack1Sound);
    }

}
