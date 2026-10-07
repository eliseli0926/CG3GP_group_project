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

public class PickupItem : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [Tooltip("Name stored in the inventory. Must match the Required Item on the lock this opens.")]
    [SerializeField] private string itemName = "Key";

    [Header("Input")]

    private bool playerInRange;

    public void Interact()
    {
        if (!playerInRange) return;
        InventoryManager.Instance.AddItem(itemName);
        Destroy(gameObject);
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
