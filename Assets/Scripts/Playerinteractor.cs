// Elise Li, Sophia Cho
// ============================================================================
// PlayerInteractor.cs
// Lets the player interact with objects by aiming the center of the screen
// (the crosshair dot) at them and clicking. Shoots a ray from the camera
// through the middle of the screen; if it hits something interactable, that
// object's Interact() is called.
//
// Attach to the Main Camera.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;   // Player/Interact (Left Mouse)

    [Header("Raycast")]
    [Tooltip("How far from the camera the ray reaches.")]
    [SerializeField] private float maxDistance = 10f;

    [Tooltip("Layers the ray can hit. Untick the Player layer so the ray doesn't hit the player.")]
    [SerializeField] private LayerMask interactLayers = ~0;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        interactAction.action.Enable();
    }

    private void Update()
    {
        if (InventoryUI.CursorFree) return;   // clicks are for the UI right now, not the world
        if (!interactAction.action.WasPressedThisFrame()) return;

        // (0.5, 0.5) is the exact center of the screen, where the crosshair is.
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Ignore trigger colliders so the ray hits the object itself, not its "nearby" zone.
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayers, QueryTriggerInteraction.Ignore))
        {
            // GetComponentInParent also works if the collider is on a child of the object.
            IInteractable target = hit.collider.GetComponentInParent<IInteractable>();
            target?.Interact();
        }
    }
}
