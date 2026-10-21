// Elise Li, Sophia Cho
// ============================================================================
// CheckpointManager.cs
// Handles setting the current room checkpoint. When the player enters a room,
// their checkpoint is set to a position in that room; if the caretaker catches
// the player, they will respawn at their last checkpoint.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [Header("Initial Settings")]
    [Tooltip("Set the player's initial spawnpoint.")]
    [SerializeField] private Transform defaultSpawnPoint;
    private Checkpoint currentCheckpoint;

    // A CheckpointManager is a singleton.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Duplicated CheckpointManager.", gameObject);
        }
    }

    // Set the checkpoint
    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint == currentCheckpoint) {return;}
        currentCheckpoint = checkpoint;
    }

    // Fetch the checkpoint
    public Transform GetRespawnPosition()
    {
        return currentCheckpoint != null ? currentCheckpoint.RespawnPoint : defaultSpawnPoint;
    }
}
