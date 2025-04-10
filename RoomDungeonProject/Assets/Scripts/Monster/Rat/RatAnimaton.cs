using UnityEngine;

public class RatAnimaton : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RatAttack()
    {
        animator.SetTrigger("RatAttack");
    
    }

    public void RatRun(bool isRun)
    {
        animator.SetBool("RatRun", isRun);
    }


}
