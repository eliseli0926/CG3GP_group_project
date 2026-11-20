// Elise Li, Sophia Cho
// ============================================================================
// ThirdPersonCamera.cs
// Third-person camera that stays behind the player and orbits around them
// with the mouse. Attach to the Main Camera (not as a child of the player).
// ============================================================================

using Unity.VisualScripting;
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

    [Header("Focus Mode")]
    [SerializeField] private float focusBlendTime = 0.4f; // how long it takes to switch focus

    private float yaw;          // left/right angle
    private float pitch = 15f;  // up/down angle

    private Rigidbody targetBody;
    private Vector3 lastTargetPosition;
    private float smoothedSpeed;

    private Transform focusPoint;
    private float focusBlend; // 0 = normal, 1 = focused on point
    public bool IsFocusing => focusPoint != null;
    private Vector3 lastFocusPosition;
    private Quaternion lastFocusRotation;

    public void SetFocus(Transform point)
    {
        focusPoint = point;
    }
    public void ClearFocus()
    {
        focusPoint = null;
    }

    private void Start()
    {
        targetBody = target.GetComponent<Rigidbody>();
        lastTargetPosition = target.position;
        lookAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;  // hide the mouse while playing
    }

    private void LateUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
        // Don't rotate the camera while the player is using menus.
        if (focusPoint == null)
        {
            Vector2 look = lookAction.action.ReadValue<Vector2>();
            yaw += look.x * mouseSensitivity;
            pitch -= look.y * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        Quaternion orbitalRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 orbitalPosition = target.position + pivotOffset - transform.forward * distance;

        float goal = focusPoint != null ? 1f : 0f;
        focusBlend = Mathf.MoveTowards(focusBlend, goal, Time.deltaTime / Mathf.Max(0.01f, focusBlendTime));

        if (focusBlend <= 0f || focusPoint == null && focusBlend <= 0f)
        {
            transform.SetPositionAndRotation(orbitalPosition, orbitalRotation);
            return;
        }

        Vector3 focusPosition = focusPoint != null ? focusPoint.position : lastFocusPosition;
        Quaternion focusRotation = focusPoint != null ? focusPoint.rotation : lastFocusRotation;
        lastFocusPosition = focusPosition;
        lastFocusRotation = focusRotation;

        float t = Mathf.SmoothStep(0f, 1f, focusBlend);
        transform.SetPositionAndRotation(
            Vector3.Lerp(orbitalPosition, focusPosition, t),
            Quaternion.Slerp(orbitalRotation, focusRotation, t)
        );
    }

}
