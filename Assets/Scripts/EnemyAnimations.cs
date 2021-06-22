using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    public float fightRotation;

    public NavMeshAgent agent;
    public float wait;

    [HideInInspector]
    public bool aggressive;

    public Animator animator;
    bool couroutineStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aggressive = true;
        animator.SetBool("isWalking", true);
    }

    void LateUpdate()
    {
        if (aggressive)
        {
            if (agent.velocity == new Vector3(0, 0, 0))
            {
                animator.SetBool("isWalking", false);
                //if is in front of player
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    //Debug.Log("Ready to hit the player");
                    if (!couroutineStarted)
                    {
                        couroutineStarted = true;
                        Invoke("Atack", wait);
                        couroutineStarted = false;
                        animator.SetBool("isAtacking", false);
                    }
                }
            }
            else
            {
                //Debug.Log("Walking");
                animator.SetBool("isWalking", true);
            }
        }
        
    }

    void Atack()
    {
        //Debug.Log("Atacking the player");
        transform.localEulerAngles = new Vector3(0, fightRotation, 0);
        animator.SetBool("isAtacking", true);
    }

    public void ForceIdle()
    {
        animator.SetBool("isAtacking", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isWalking", false);
        aggressive = false;
    }

    public void Die()
    {
        animator.SetBool("isDeath", true);
    }

    public void ShowHitAnimation()
    {
        animator.SetBool("isHit", true);
        StartCoroutine(FinishHitAnimation());
    }

    IEnumerator FinishHitAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isHit", false);
    }

}

