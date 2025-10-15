using UnityEngine;
using UnityEngine.AI;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }   
}
