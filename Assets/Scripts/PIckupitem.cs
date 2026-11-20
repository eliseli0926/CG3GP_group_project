// Elise Li, Sophia Cho
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

    [Header("Sound")]
    [Tooltip("Plays when the item is picked up.")]
    [SerializeField] private AudioClip pickupSound;
    [Tooltip("Volume of the pickup sound.")]
    [Range(0f, 1f)] [SerializeField] private float pickupVolume = 1f;

    private bool playerInRange;

    // The item will physically disappear upon being picked up and added to inventory.
    public void Interact()
    {
        if (!playerInRange) return;
        InventoryManager.Instance.AddItem(itemName);

        // PlayClipAtPoint spawns a temporary audio object, so the sound keeps playing
        // even though this item is destroyed right after.
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);

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
