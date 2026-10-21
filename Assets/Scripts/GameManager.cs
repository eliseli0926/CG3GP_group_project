// Elise Li, Sophia Cho
// ============================================================================
// GameManager.cs
// Handles loss conditions for respawning the player and resetting positions
// ============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Settings")]
    [Tooltip("Set this to the loss panel in the Canvas.")]
    public GameObject losePanel;
    public GameObject player;
    public Transform playerSpawnPoint;

    private bool isGameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Duplicated GameManager.", gameObject);
        }
        Time.timeScale = 1f;
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Later, animations for the enemy catching the player will be displayed before the lose panel shows up

        losePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RespawnFromCheckpoint()
    {
        TeleportPlayer(CheckpointManager.Instance.GetRespawnPosition());
        ResetEnemy();
        ResetGameMode();
    }

    // Move the player back. A CharacterController must be disabled
    // while teleporting or it snaps the player back.
    void TeleportPlayer(Transform spawn)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        player.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        if (cc != null) cc.enabled = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Send every enemy back to where it started
    void ResetEnemy()
    {
        foreach (EnemyTouchPlayer enemy in FindObjectsByType<EnemyTouchPlayer>(FindObjectsSortMode.None))
            enemy.ResetEnemy();
    }

    void ResetGameMode()
    {
        losePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isGameOver = false;
    }
}
