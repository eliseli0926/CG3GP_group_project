// Elise Li, Sophia Cho
// ============================================================================
// PageContents.cs
// Displays the text contents of the page when the player clicks on a note.
// The player presses Esc to leave the page.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageContents : MonoBehaviour
{
    public static PageContents Instance {get; private set;}

    [Header("UI")]
    [Tooltip("The panel that holds the page contents.")]
    [SerializeField] private GameObject pagePanel;
    [Tooltip("Text inside the page.")]
    [SerializeField] private TMP_Text pageText;

    void Start()
    {
        Instance = this;
        pagePanel.SetActive(false);
    }

    public void Show(string message)
    {
        pageText.text = message;
        pagePanel.SetActive(true);
    }

    public void Hide(string message)
    {
        pagePanel.SetActive(false);
    }
}
