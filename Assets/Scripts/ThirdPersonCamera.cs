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

    [Header("Walk Bob")]
    [SerializeField] private float bobAmplitude = 0.05f;  // how far up/down the camera moves
    [SerializeField] private float bobFrequency = 10f;    // how fast it bobs (higher = quicker steps)
    [SerializeField] private float bobFadeSpeed = 4f;     // how quickly the bob starts/stops

    private float yaw;          // left/right angle
    private float pitch = 15f;  // up/down angle

    private Rigidbody targetBody;
    private float bobTimer;     // where we are in the sine wave
    private float bobStrength;  // 0 = standing still, 1 = full bob
    private Vector3 lastTargetPosition;
    private float smoothedSpeed;

    private void Start()
    {
        targetBody = target.GetComponent<Rigidbody>();
        lastTargetPosition = target.position;
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
        transform.position = target.position + pivotOffset - transform.forward * distance
                             + Vector3.up * GetBobOffset();
    }

    // Returns how far up/down to shift the camera this frame. Only bobs while the
    // player is walking on the ground, and fades in/out so it never snaps.
    private float GetBobOffset()
    {
        // Use how far the player ACTUALLY moved, not the Rigidbody's velocity: PlayerMovement
        // keeps setting velocity even when the player is pushing against a wall or table.
        Vector3 delta = (target.position - lastTargetPosition) / Mathf.Max(Time.deltaTime, 0.0001f);
        lastTargetPosition = target.position;

        // Smooth the measured speed, since physics may not move the player every rendered frame.
        float horizontalSpeed = new Vector2(delta.x, delta.z).magnitude;
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, horizontalSpeed, 10f * Time.deltaTime);
        bool grounded = targetBody == null || Mathf.Abs(targetBody.velocity.y) < 0.5f;
        bool walking = smoothedSpeed > 1f && grounded;  // really moving and not jumping/falling

        bobStrength = Mathf.MoveTowards(bobStrength, walking ? 1f : 0f, bobFadeSpeed * Time.deltaTime);
        if (walking) bobTimer += Time.deltaTime * bobFrequency;

        return Mathf.Sin(bobTimer) * bobAmplitude * bobStrength;
    }
}
