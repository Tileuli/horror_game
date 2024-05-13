using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public Animator animator;

    public float chaseRange = 10f;
    public float deathRange = .75f;
    public Transform[] points;

    private NavMeshAgent nav;
    private int destPoint = 0;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if(distance < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrolling();
        }
        
        PlayerDeath(distance);
    }

    void GoToNextPoint()
    {
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    private void PlayerDeath(float distance)
    {
        if (distance < deathRange)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Patrolling()
    {
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }

        animator.SetBool("isRunning", true);
    }

    private void ChasePlayer()
    {
        nav.isStopped = false;
        nav.SetDestination(player.position);

        animator.SetBool("isRunning", true);
    }
}
