// Elise Li, Sophia Cho
// ============================================================================
// DeadNPC.cs
// A body the player can search. The book page next to it starts hidden;
// clicking the body reveals the page. It can only be searched once, after
// which the "Search" prompt no longer shows.
//
// Attach to the dead NPC (the object with its collider), along with an
// InteractPrompt set to "Search".
// ============================================================================

using UnityEngine;

public class DeadNPC : MonoBehaviour, IInteractable
{
    [Tooltip("The BookPageWriting object to reveal when the body is searched.")]
    [SerializeField] private GameObject bookPage;

    private bool searched;

    private void Start()
    {
        bookPage.SetActive(false);   // hidden until the body is searched
    }

    // Called by PlayerInteractor when the player clicks the body.
    public void Interact()
    {
        if (searched) return;
        searched = true;

        bookPage.SetActive(true);

        // Stop showing "Search" now that there's nothing left to find.
        InteractPrompt prompt = GetComponent<InteractPrompt>();
        if (prompt != null) prompt.enabled = false;
    }
}