// ============================================================================
// DoorInteractable.cs
// A door that the player can open/close. When the player is close enough and presses
// Interact, the door opens/closes. It may require an item the user is carrying in
// their inventory, which is consumed on use if it is there. Afterwards, the door
// is permanently unlocked.
//
// Needs a trigger collider (Is Trigger ticked) to detect the player nearby.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string requiredItemName = "Key";
    [SerializeField] private float openAngle = 90f;

    private bool playerInRange;
    private bool isOpen;
    private bool isUnlocked;

    // Will likely later add some animation component once we learn it, but for now the door will instantly open/close.
    // Any door that has no required item name can be opened unconditionally.
    public void Interact()
    {
        if (!playerInRange) return;
        if (isOpen)
        {
            transform.Rotate(0f, -openAngle, 0f);
            isOpen = false;
            return;
        }
        if (isUnlocked || requiredItemName == "")
        {
            transform.Rotate(0f, openAngle, 0f);
            isOpen = true;
            return;
        }
        if (InventoryManager.Instance.HasItem(requiredItemName)) {
            InventoryManager.Instance.RemoveItem(requiredItemName);
            transform.Rotate(0f, openAngle, 0f);
            isOpen = true;
            isUnlocked = true;
        };
    }


    // Trigger events: fire when the player walks into / out of the door's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
