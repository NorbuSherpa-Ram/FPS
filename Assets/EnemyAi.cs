using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public GameObject target;

    public float timeToFollow = 0.5f;
    float timer;
  

    [Header("Patroling ")]
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;


    [Header("Attack ")]
    bool isAttacked ;
    bool inAttackRange;
    public float attackRange = 15f;
   

    [Header("Chase ")]
    bool isChasing;
    public float chaseRange = 25f;
    public LayerMask whatIsPlayer;
    
    [Header("Ground Check ")]
    public Transform groundCheck;
    public float groundCheckLength = 2.5f;
    public LayerMask whatIsGround;

    [Header("Player info")]
    Transform playerPos;
    void Start()
    {
        timer = timeToFollow;
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
        isChasing = Physics.CheckSphere(transform.position,chaseRange, whatIsPlayer );
        inAttackRange  = Physics.CheckSphere(transform.position,attackRange, whatIsPlayer );

        if(!isChasing  && !inAttackRange  )Patroling();
        if (!isAttacked && isChasing) ChasePlayer();
        if (isChasing && inAttackRange) Attack();
        
    }
    void Attack()
    {
        agent.SetDestination(transform.position);
        Debug.Log("Attacking");
    }
    void ChasePlayer()
    {
        agent.SetDestination(playerPos.position);
    }
    void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            float distance = Vector3.Distance(transform.position, walkPoint);
            if (distance <= 1f)
            {
                walkPointSet = false;
                return;
            }
        }
    }
    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(groundCheck.position, -transform.up, groundCheckLength, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(groundCheck.position, -transform.up);
        Gizmos.DrawRay(transform .position, transform.forward);
        Gizmos.DrawWireSphere(transform.position, attackRange );
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
