using UnityEngine;
using UnityEngine.AI;

public class EnemyTouchPlayer : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private NavMeshAgent agent;
    private EnemyFSM fsm;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        fsm = GetComponentInChildren<EnemyFSM>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.Lose();
    }

    public void ResetEnemy()
    {
        agent.Warp(startPosition);
        transform.rotation = startRotation;
        if (fsm != null) fsm.ResetState();
    }
}