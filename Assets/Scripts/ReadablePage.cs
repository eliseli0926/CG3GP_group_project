// Elise Li, Sophia Cho
// ============================================================================
// ReadablePage.cs
// A note page the user can click on to view. Cannot be picked up, but can be
// viewed multiple times.
// Needs a trigger collider (Is Trigger ticked) to detect the player nearby.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadablePage : MonoBehaviour, IInteractable
{
[Header("Item")]
    [Tooltip("Contents of the page")]
    [SerializeField] private string pageContents;

    [Header("Input")]
    private bool playerInRange;

    // The page will display in the UI
    public void Interact()
    {
        if (!playerInRange) return;
        // Do the stuff
    }

    // Trigger events: fire when the player walks into / out of the item's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
