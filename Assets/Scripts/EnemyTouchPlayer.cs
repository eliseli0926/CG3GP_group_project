// Elise Li, Sophia Cho
// ============================================================================
// EnemyTouchPlayer.cs
// If the enemy's trigger collider intersects with the player's collider
// (enemy too close) and the enemy can see the player (not blocked behind an
// obstacle), the player loses.
// ============================================================================

using UnityEngine;
using UnityEngine.AI;

public class EnemyTouchPlayer : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private NavMeshAgent agent;
    private EnemyFSM fsm;
    [Tooltip("Set to this enemy's Sight script.")]
    [SerializeField] private Sight sightSensor;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        fsm = GetComponentInChildren<EnemyFSM>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sightSensor.detectedObject != null)
            GameManager.Instance.Lose();
    }

    public void ResetEnemy()
    {
        agent.Warp(startPosition);
        transform.rotation = startRotation;
        if (fsm != null) fsm.ResetState();
    }
}
