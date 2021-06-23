using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int lifes;
    
    public float lookRadius = 30f;
    Transform target;
    NavMeshAgent agent;
    [SerializeField] EnemyAnimations animations;

    private GameManager gameManager;

    private bool inmune;
    private bool death;

    private static AudioSource source;
    [SerializeField] AudioClip[] punchClips;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lifes = 5;
        inmune = false;
        death = false;
        source = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //Debug.Log("Moving");
            if(distance <= agent.stoppingDistance)
            {
                // Atack the target

                // Face the target
                FaceTarget();
            }
        }

        /*if(agent.velocity == new Vector3(0, 0, 0)){
            Debug.Log("Stop");    
        }else{
            Debug.Log(agent.velocity);
        }*/
    }

    void FaceTarget()
    {
        //Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized; 
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void PlayPunchSound()
    {
        int punchIndex = Random.Range(0, punchClips.Length);
        AudioClip punch = punchClips[punchIndex];
        source.PlayOneShot(punch);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animations.aggressive)
        {
            if (other.CompareTag("Hand"))
            {                            
                if (!inmune && !death)
                {
                    PlayPunchSound();
                    lifes--;
                    inmune = true;

                    animations.ShowHitAnimation();

                    StartCoroutine(QuitInmunity());

                    if (lifes <= 0)
                    {
                        death = true;
                        animations.Die();
                        StartCoroutine(DeleteObject());
                        gameManager.DefeatEnemy();
                    }
                }
            }
        }
    }

    public void ForceIdle()
    {
        animations.ForceIdle();
        Transform idlePoint = GameObject.Find("IdlePoint").transform;
        agent.SetDestination(idlePoint.position);
    }

    IEnumerator QuitInmunity()
    {
        yield return new WaitForSeconds(2f);
        inmune = false;
    }

    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
