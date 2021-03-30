using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5.0f;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false; 

    NavMeshAgent nMa; 

    // Start is called before the first frame update
    void Start()
    {
        nMa = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= nMa.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= nMa.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        nMa.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        //ttemp
        print(name + "is attacking " + target.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
