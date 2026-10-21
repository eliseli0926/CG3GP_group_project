using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject losePanel;
    private bool isGameOver;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;

        losePanel.SetActive(true);
        Time.timeScale = 0f;                     // freeze the game
        Cursor.lockState = CursorLockMode.None;  // so you can click the button
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}