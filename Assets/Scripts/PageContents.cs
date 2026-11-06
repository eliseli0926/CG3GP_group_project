// Elise Li, Sophia Cho
// ============================================================================
// PageContents.cs
// Displays the text contents of the page when the player clicks on a note.
// The player presses Esc to leave the page.
// Only one is needed; every ReadablePage in the scene uses it.
//
// Attach to the Canvas.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PageContents : MonoBehaviour
{
    public static PageContents Instance { get; private set; }

    [Header("UI")]
    [Tooltip("The panel that holds the page contents.")]
    [SerializeField] private GameObject pagePanel;
    [Tooltip("Text inside the page.")]
    [SerializeField] private TMP_Text pageText;

    // True while a page is showing.
    public bool IsOpen => pagePanel.activeSelf;

    // Awake runs before any Start, so Instance is ready before other scripts need it.
    void Awake()
    {
        Instance = this;
        pagePanel.SetActive(false);
    }

    void Update()
    {
        // Esc closes the page.
        if (IsOpen && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Hide();
        }
    }

    public void Show(string message)
    {
        pageText.text = message;
        pagePanel.SetActive(true);
    }

    public void Hide()
    {
        pagePanel.SetActive(false);
    }
}