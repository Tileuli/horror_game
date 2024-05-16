using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public PlayerHealth playerHealth;
    public int damage = 25;
    public Animator animator;

    public float chaseRange = 10f;
    public float damageRange = 1.0f;
    public Transform[] points;

    private NavMeshAgent nav;
    private int destPoint = 0;

    public float damageRate = 1f;

    private float lastDamageTime;

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
        
        PlayerDamage(distance);
    }

    void GoToNextPoint()
    {
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    private void PlayerDamage(float distance)
    {
        if (Time.time - lastDamageTime > damageRate)
        {
            if (distance < damageRange)
            {
                playerHealth.TakeDamage(damage);

                lastDamageTime = Time.time;
            }
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
