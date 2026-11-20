// Elise Li, Sophia Cho
// ============================================================================
// ChestInteractable.cs
// A chest that the player can search. If it is locked, a MessagePopup will
// display with the relevant message. If it is unlocked, the player will search
// the chest, and any of its contents will be displayed.
// ============================================================================

using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [Tooltip("Contents of the chest, hidden until the chest is searched")]
    [SerializeField] private GameObject contents;
    [Tooltip("Set to true if the chest must be unlocked")]
    [SerializeField] private bool startLocked = true;
    [Tooltip("The message to display if the player tries to open a locked chest")]
    [SerializeField] private string lockedMessage = "It's locked. There's a keypad on it.";

    private bool unlocked;
    private bool searched;
    private InteractPrompt prompt;

    private void Start()
    {
        prompt = GetComponent<InteractPrompt>();
        if (contents != null) contents.SetActive(false);

        unlocked = !startLocked;
        if (prompt != null) prompt.enabled = unlocked;
    }

    public void Interact()
    {
        if (searched) return;
        if (!unlocked)
        {
            if (MessagePopup.Instance != null) MessagePopup.Instance.Show(lockedMessage);
            return;
        }

        searched = true;
        if (contents != null) contents.SetActive(true);
        if (prompt != null) prompt.enabled = false;
    }

    public void Unlock()
    {
        unlocked = true;
        if (prompt != null && !searched) prompt.enabled = true;
    }
}
