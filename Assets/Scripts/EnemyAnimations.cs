using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    public NavMeshAgent agent;
    public float wait;

    private Animator animator;
    bool couroutineStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
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

    void Atack()
    {
        //Debug.Log("Atacking the player");
        animator.SetBool("isAtacking", true);
    }

}

