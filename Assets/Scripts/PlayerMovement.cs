// Elise Li, Sophia Cho
// ============================================================================
// PlayerMovement.cs
// Moves the player with WASD relative to the camera and jumps with Space.
// Uses a Rigidbody so the player collides with the room through physics.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 5f;

    [Tooltip("How far down to check for ground: about half the player's height plus a little.")]
    [SerializeField] private float groundCheckDistance = 1.1f;

    private Rigidbody body;
    private Transform cam;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;   // stop physics from tipping the player over
        cam = Camera.main.transform;

        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void Update()
    {

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Camera's forward/right with the up/down tilt removed, so W always means "away from the camera".
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        Vector3 move = (forward * input.y + right * input.x) * moveSpeed;

        // Keep the current vertical velocity so gravity and jumping still work.
        body.velocity = new Vector3(move.x, body.velocity.y, move.z);

        // Face the direction we're walking.
        if (move != Vector3.zero) transform.forward = move;

        if (jumpAction.action.WasPressedThisFrame() && IsGrounded())
        {
            body.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }
    }

    // Shoots a short ray straight down to see if the player is standing on something.
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
