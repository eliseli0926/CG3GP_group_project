// Elise Li, Sophia Cho
// ============================================================================
// InventoryUI.cs
// Shows the player's inventory on screen. Press I to open a panel listing the
// names of all items picked up, and press I again to close it.
//
// Attach to the Canvas.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("The inventory panel that opens and closes.")]
    [SerializeField] private GameObject inventoryPanel;

    [Tooltip("Text inside the panel that lists the item names.")]
    [SerializeField] private TMP_Text itemListText;

    private void Start()
    {
        inventoryPanel.SetActive(false);

        // Keep the cursor hidden and locked to the center for the whole game.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    // Opens the panel if it's closed, closes it if it's open.
    private void ToggleInventory()
    {
        bool open = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(open);
        if (open) RefreshList();
    }

    // Rewrites the panel text with one item name per line.
    private void RefreshList()
    {
        var items = InventoryManager.Instance.Items;
        itemListText.text = items.Count == 0 ? "(empty)" : string.Join("\n", items);
    }
}