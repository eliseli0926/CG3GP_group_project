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

    [Header("Hearing")]
    [Tooltip("The enemy hears the player walking within this many meters, in any direction. Crouching is silent.")]
    [SerializeField] private float hearingRange = 5f;

    [Header("Doors")]
    [Tooltip("How close (in meters) a closed door must be ahead for the enemy to stop at it.")]
    [SerializeField] private float doorCheckDistance = 1.5f;

    private int currentPoint;
    private float waitTimer;
    private PlayerMovement player;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>();
        GoToCurrentPoint();
    }

    bool HearsPlayer()
    {
        return player != null && player.IsMakingNoise
            && Vector3.Distance(agent.transform.position, player.transform.position) <= hearingRange;
    }

    bool CanSensePlayer()
    {
        return sightSensor.detectedObject != null || HearsPlayer();
    }

    void Update()
    {
        if (currentState == EnemyState.Patrol) { Patrol(); }
        else { ChasePlayer(); }
    }

    void Patrol()
    {
        if (CanSensePlayer())
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
        if (!CanSensePlayer())
        {
            currentState = EnemyState.Patrol;
            GoToCurrentPoint();   // lost the player, go back to the route
            return;
        }

        if (agent.isOnNavMesh)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.transform.position);

            // Wait at closed doors instead of walking through them
            bool blocked = ClosedDoorAhead();
            agent.isStopped = blocked;
            if (blocked) agent.velocity = Vector3.zero;
        }
    }

    // Checks toward the next corner of the enemy's path for a closed door.
    // Door colliders are triggers, so the ray has to include triggers.
    bool ClosedDoorAhead()
    {
        Vector3 origin = agent.transform.position + Vector3.up;   // about waist height
        Vector3 direction = agent.steeringTarget - agent.transform.position;
        direction.y = 0f;
        if (direction == Vector3.zero) return false;

        RaycastHit[] hits = Physics.RaycastAll(origin, direction.normalized, doorCheckDistance, ~0, QueryTriggerInteraction.Collide);
        foreach (RaycastHit hit in hits)
        {
            DoorInteractable door = hit.collider.GetComponentInParent<DoorInteractable>();
            if (door != null && !door.IsOpen) return true;
        }
        return false;
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
        // Hearing range (yellow circle)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);

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