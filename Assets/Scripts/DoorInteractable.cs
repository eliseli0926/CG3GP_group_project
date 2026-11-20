// Elise Li, Sophia Cho
// ============================================================================
// DoorInteractable.cs
// A door that the player can open/close. When the player is close enough and presses
// Interact, the door opens/closes. It may require an item the user is carrying in
// their inventory, which is consumed on use if it is there. Afterwards, the door
// is permanently unlocked.
//
// The player must be within interactRange of the door's pivot (its hinge) to use it.
// A distance check is used instead of a trigger collider because a trigger on the
// door would swing away with it when it opens, so the player couldn't close it again.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Input")]
    [Tooltip("The item required to open the door.")]
    [SerializeField] private string requiredItemName = "Key";
    [Tooltip("The amount the door can open.")]
    [SerializeField] private float openAngle = 90f;
    [Tooltip("The message to show when the player doesn't have the right item.")]
    [SerializeField] private string lockedMessage = "This door is locked... I don't have the right key.";
    [Tooltip("How close (in meters) the player must be to the door's pivot to open/close it.")]
    [SerializeField] private float interactRange = 5f;

    [Header("Animation")]
    [Tooltip("Speed of door swing, in degrees per second")]
    [SerializeField] private float swingSpeed = 150f;

    // Internal variables used to check if the user can open/close the door
    private Transform player;
    private bool isOpen;
    private bool isUnlocked;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine swingRoutine;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null) player = playerObject.transform;

        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    private bool PlayerInRange()
    {
        return player != null && Vector3.Distance(player.position, transform.position) <= interactRange;
    }

    // Any door that has no required item name can be opened unconditionally and is treated as an unlocked door.
    public void Interact()
    {
        if (!PlayerInRange()) return;
        // Close the door
        if (isOpen)
        {
            SetOpen(false);
            return;
        }
        // Unlocked door that is closed
        if (isUnlocked || requiredItemName == "")
        {
            SetOpen(true);
            return;
        }
        // If the user has the right item, consume it from the inventory and unlock and open the door.
        if (InventoryManager.Instance.HasItem(requiredItemName)) {
            InventoryManager.Instance.RemoveItem(requiredItemName);
            SetOpen(true);
            isUnlocked = true;
            return;
        };
        MessagePopup.Instance.Show(lockedMessage);
    }

    // Shows the interact range in the Scene view when the door is selected.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    // Handles opening and closing, and calls the door animation.
    // If the door is in the middle of opening/closing, it stops the current animation
    // and starts the movement in the opposite direction
    private void SetOpen(bool open)
    {
        isOpen = open;
        if (swingRoutine != null) StopCoroutine(swingRoutine);
        swingRoutine = StartCoroutine(SwingTo(open ? openRotation : closedRotation));
    }

    // Door animation
    private IEnumerator SwingTo(Quaternion target)
    {
        while (Quaternion.Angle(transform.localRotation, target) > 0.01f)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, target, swingSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.localRotation = target;
        swingRoutine = null;
    }
}
