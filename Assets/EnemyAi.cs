using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent  agent;
    public GameObject target;

    public float timeToFollow =0.5f;
    float timer;
    void Start()
    {
        timer = timeToFollow;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFollow<=0)
        {
            agent.SetDestination(target.transform.position);
            timeToFollow = timer;
        }
        else
        {
            timeToFollow -= Time.deltaTime;
        }

        
    }
}
