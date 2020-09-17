using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigationScript : MonoBehaviour
{

    NavMeshAgent nm;
    public enum AIState { attacking, chasing };
    Animator m_Animator;

    [Header("AI settings")]
    public AIState aiState = AIState.chasing;
    public float distanceAttack = 10f;

    [Header("Target settings")]
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Think()
    {
        while(true)
        {
            switch(aiState)
            {
                case AIState.attacking:

                    float dist = Vector3.Distance(target.position, transform.position);

                    if (dist >= distanceAttack)
                    {
                        m_Animator.SetTrigger("Z_Run");
                        aiState = AIState.chasing;
                    }

                    nm.SetDestination(transform.position);
                    break;
                case AIState.chasing:

                    dist = Vector3.Distance(target.position, transform.position);

                    if (dist < distanceAttack)
                    {
                        m_Animator.SetTrigger("Z_Attack");
                        aiState = AIState.attacking;
                    }

                    nm.SetDestination(target.position);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
