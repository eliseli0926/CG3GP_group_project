// Elise Li, Sophia Cho
// ============================================================================
// EnemyFSM.cs
// Controls the enemy's actions, between patrolling the hallways and chasing
// the player. The player loses and respawns if the enemy's trigger collider
// intersects with the player's collider (see EnemyTouchPlayer / GameManager).
// ============================================================================

using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private enum EnemyState { Patrol, ChasePlayer }
    [Header("(View-only) Enemy State")]
    [SerializeField] private EnemyState currentState;

    [Header("Enemy AI")]
    [Tooltip("Set this to the attached Sight script.")]
    [SerializeField] private Sight sightSensor;
    [Tooltip("Set this to the parent object's NavMeshAgent.")]
    [SerializeField] private NavMeshAgent agent;

    [Header("Patrol")]
    [Tooltip("Empty objects placed along the hallways, in the order to visit them.")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitAtPoint = 1f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;

    private int currentPoint;
    private float waitTimer;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        GoToCurrentPoint();
    }

    void Update()
    {
        if (currentState == EnemyState.Patrol) { Patrol(); }
        else { ChasePlayer(); }
    }

    void Patrol()
    {
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            return;
        }

        if (!agent.isOnNavMesh || patrolPoints.Length == 0) return;

        agent.isStopped = false;
        agent.speed = patrolSpeed;

        if (agent.pathPending) return;

        // Reached the current point: wait a moment, then head to the next one
        if (agent.remainingDistance <= agent.stoppingDistance + 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitAtPoint)
            {
                waitTimer = 0f;
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                GoToCurrentPoint();
            }
        }
    }

    void ChasePlayer()
    {
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Patrol;
            GoToCurrentPoint();   // lost the player, go back to the route
            return;
        }

        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.speed = chaseSpeed;
            agent.SetDestination(sightSensor.detectedObject.transform.position);
        }
    }

    void GoToCurrentPoint()
    {
        if (agent == null || !agent.isOnNavMesh || patrolPoints.Length == 0) return;
        waitTimer = 0f;
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }

    public void ResetState()
    {
        currentState = EnemyState.Patrol;
        currentPoint = 0;
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
        GoToCurrentPoint();
    }

    // Draws the patrol route in the Scene view when the enemy is selected
    void OnDrawGizmosSelected()
    {
        if (patrolPoints == null || patrolPoints.Length < 2) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (patrolPoints[i] == null) continue;
            Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);
            Transform next = patrolPoints[(i + 1) % patrolPoints.Length];
            if (next != null) Gizmos.DrawLine(patrolPoints[i].position, next.position);
        }
    }
}