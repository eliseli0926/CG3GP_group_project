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
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 5f;

    [Tooltip("How far down to check for ground: about half the player's height plus a little.")]
    [SerializeField] private float groundCheckDistance = 1.1f;

    [Header("Footsteps")]
    [Tooltip("Footstep sound that loops while the player is walking.")]
    [SerializeField] private AudioClip footstepLoop;

    private AudioSource footstepSource;
    private Rigidbody body;
    private Transform cam;
    private Animator animator;
    private bool controlEnabled = true;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;   // stop physics from tipping the player over
        cam = Camera.main.transform;

        animator = GetComponentInChildren<Animator>();

        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.clip = footstepLoop;
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;

        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void Update()
    {
        Vector2 input = controlEnabled ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;

        // Camera's forward/right with the up/down tilt removed, so W always means "away from the camera".
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        Vector3 move = (forward * input.y + right * input.x) * moveSpeed;

        // Keep the current vertical velocity so gravity and jumping still work.
        body.velocity = new Vector3(move.x, body.velocity.y, move.z);

        // Face the direction we're walking.
        if (move != Vector3.zero) transform.forward = move;

        if (animator != null) animator.SetFloat("Speed", move.magnitude);

        // Loop footsteps while walking on the ground; stop when standing still or in the air.
        bool walking = move != Vector3.zero && IsGrounded();
        if (walking && !footstepSource.isPlaying) footstepSource.Play();
        else if (!walking && footstepSource.isPlaying) footstepSource.Stop();

        if (controlEnabled && jumpAction.action.WasPressedThisFrame() && IsGrounded())
        {
            body.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            if (animator != null) animator.SetTrigger("Jump");
        }
    }

    // Shoots a short ray straight down to see if the player is standing on something.
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    // Call when freezing/unfreezing player movement
    public void SetControlEnabled(bool value)
    {
        controlEnabled = value;
    }
}
