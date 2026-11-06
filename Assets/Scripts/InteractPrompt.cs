// Elise Li, Sophia Cho
// ============================================================================
// InteractPrompt.cs
// Holds the word shown on screen when the crosshair is on this object
// (e.g. "Search", "Pick up", "Open"). PlayerInteractor reads it.
// Untick the component (or disable it from code) to stop showing the prompt.
//
// Attach to any interactable object that should show a prompt.
// ============================================================================

using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [Tooltip("Text shown under the crosshair when aiming at this object.")]
    [SerializeField] private string promptText = "Search";

    // Other scripts (PlayerInteractor) can read the text but not change it.
    public string PromptText => promptText;
}