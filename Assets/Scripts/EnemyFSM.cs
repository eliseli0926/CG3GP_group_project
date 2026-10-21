// Elise Li, Sophia Cho
// ============================================================================
// EnemyFSM.cs
// Controls the enemy's actions, between patrolling the hallways and chasing
// the player. The player will instantly lose and be reset to their last
// checkpoint (NOT YET IMPLEMENTED) if the enemy's trigger collider intersects
// with the player's collider.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (currentState == EnemyState.Patrol) { Patrol(); }
        else { ChasePlayer(); }
    }

    void Patrol()
    {
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            // TODO: determine hall-patrolling behavior
        }

        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ChasePlayer()
    {
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Patrol;
            return;
        }

        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.SetDestination(sightSensor.detectedObject.transform.position);
        }

        // float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        // TODO: may need to write an additional script for GameManager to handle loss conditions, etc.
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
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
    }
}
