// Elise Li, Sophia Cho
// ============================================================================
// MainMenu.cs
// The start menu. Shows a Start button with the controls listed underneath.
// Clicking Start loads the game scene.
//
// Attach to the Canvas in the Menu scene.
// ============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Exact name of your game scene (as it appears in the Project window, without .unity).")]
    [SerializeField] private string gameSceneName = "SampleScene";

    [Tooltip("Text under the Start button. The script fills it in for you.")]
    [SerializeField] private TMP_Text instructionsText;

    [Tooltip("Seconds to wait after clicking Start, so the click sound can play.")]
    [SerializeField] private float startDelay = 0.5f;

    private void Start()
    {
        // The game scene locks and hides the cursor, so make sure it's
        // visible here so the player can click the button.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        StartCoroutine(LoadAfterDelay());
    }
    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        SceneManager.LoadScene(gameSceneName);
    }
}