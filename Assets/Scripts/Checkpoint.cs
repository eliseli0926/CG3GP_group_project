// Elise Li, Sophia Cho
// ============================================================================
// Checkpoint.cs
// Used for all checkpoints. Make sure the checkpoint object has a trigger
// collider attached, and make sure the player has a collider.
// Stores the respawn point for a given room. When the player enters the room,
// sets the CheckpointManager's checkpoint to this one.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [Tooltip("Optionally create a child object within this checkpoint to set the respawn location. Otherwise, it will use this object's location.")]
    [SerializeField] private Transform customRespawnPoint;

    public Transform RespawnPoint => customRespawnPoint != null ? customRespawnPoint : transform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(this);
        }
    }

}
