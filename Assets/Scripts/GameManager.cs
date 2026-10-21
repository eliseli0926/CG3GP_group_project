using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject losePanel;
    public GameObject player;
    public Transform playerSpawnPoint;

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
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        // Move the player back. A CharacterController must be disabled
        // while teleporting or it snaps the player back.
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        player.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
        if (cc != null) cc.enabled = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Send every enemy back to where it started
        foreach (EnemyTouchPlayer enemy in FindObjectsByType<EnemyTouchPlayer>(FindObjectsSortMode.None))
            enemy.ResetEnemy();

        losePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;            
        isGameOver = false;
    }
}