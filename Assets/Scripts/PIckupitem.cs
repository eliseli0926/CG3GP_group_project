// ============================================================================
// PickupItem.cs
// An item the player can pick up (key, fuse, crank handle...). When the player
// is close enough and presses Interact, the item goes into the inventory and
// is removed from the scene.
//
// Needs a trigger collider (Is Trigger ticked) to detect the player nearby.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour
{
    [Header("Item")]
    [Tooltip("Name stored in the inventory. Must match the Required Item on the lock this opens.")]
    [SerializeField] private string itemName = "Key";

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction; 

    private bool playerInRange;

    private void Start()
    {
        interactAction.action.Enable();
    }

    private void Update()
    {
        if (playerInRange && interactAction.action.WasPressedThisFrame())
        {
            InventoryManager.Instance.AddItem(itemName);
            Destroy(gameObject);   // remove the item from the world
        }
    }

    // Trigger events: fire when the player walks into / out of the item's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}