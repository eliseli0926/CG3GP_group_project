// Elise Li, Sophia Cho
// ============================================================================
// ThirdPersonCamera.cs
// Third-person camera that stays behind the player and orbits around them
// with the mouse. Attach to the Main Camera (not as a child of the player).
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 pivotOffset = new Vector3(0f, 1.6f, 0f);

    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float mouseSensitivity = 0.15f;

    [Header("Orbit")]
    [SerializeField] private float distance = 4f;
    [SerializeField] private float minPitch = -30f;  // how far below the player the camera can go
    [SerializeField] private float maxPitch = 60f;   // how far above

    private float yaw;          // left/right angle
    private float pitch = 15f;  // up/down angle

    private void Start()
    {
        lookAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;  // hide the mouse while playing
    }

    private void LateUpdate()
    {
        // Don't rotate the camera while the player is using menus.
        Vector2 look = InventoryUI.CursorFree ? Vector2.zero : lookAction.action.ReadValue<Vector2>();
        yaw += look.x * mouseSensitivity;
        pitch -= look.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = target.position + pivotOffset - transform.forward * distance;
    }
}
