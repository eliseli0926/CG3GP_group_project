// Elise Li, Sophia Cho
// ============================================================================
// NPCAnimator.cs
// Makes an NPC's Idle / Walk animations match how it's actually moving.
// Each frame it measures how far the NPC moved and sends that speed to the
// Animator's "Speed" parameter. Works no matter how the NPC is moved
// (NavMeshAgent, Rigidbody, or another script changing its position).
//
// Attach to the NPC's top-level object (the one that moves).
// ============================================================================

using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        // How far we moved since last frame, ignoring up/down (so falling doesn't count as walking).
        Vector3 moved = transform.position - lastPosition;
        moved.y = 0f;

        // Distance / time = speed.
        float speed = Time.deltaTime > 0f ? moved.magnitude / Time.deltaTime : 0f;

        if (animator != null) animator.SetFloat("Speed", speed);

        lastPosition = transform.position;
    }
}