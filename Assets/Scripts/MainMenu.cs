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

public class MainMenu : MonoBehaviour
{
    [Tooltip("Exact name of your game scene (as it appears in the Project window, without .unity).")]
    [SerializeField] private string gameSceneName = "SampleScene";

    [Tooltip("Text under the Start button. The script fills it in for you.")]
    [SerializeField] private TMP_Text instructionsText;

    private void Start()
    {
        // The game scene locks and hides the cursor, so make sure it's
        // visible here so the player can click the button.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        instructionsText.text =
            "W A S D  -  Move\n" +
            "Space  -  Jump\n" +
            "Mouse  -  Look around\n" +
            "Left Click  -  Interact / pick up\n" +
            "I  -  Open / close inventory";
    }

    // Hooked up to the Start button's On Click.
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}