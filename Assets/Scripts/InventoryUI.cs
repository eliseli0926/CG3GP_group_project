// Elise Li, Sophia Cho
// ============================================================================
// InventoryUI.cs
// Shows the player's inventory on screen. Pressing Esc frees the mouse cursor
// so the player can click the Inventory button (top right), which opens a
// panel listing the names of all items picked up. Pressing Esc again hides the
// cursor, closes the panel, and returns to normal play.
//
// Attach to the Canvas.
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    // True while the cursor is free for clicking menus. ThirdPersonCamera and
    // PlayerInteractor check this so the camera doesn't spin and clicks don't
    // pick things up while the player is using the UI.
    public static bool CursorFree { get; private set; }

    [Header("Input")]
    [SerializeField] private InputActionReference menuAction;   // Player/Menu (Esc)

    [Header("UI")]
    [Tooltip("The inventory panel that opens and closes.")]
    [SerializeField] private GameObject inventoryPanel;

    [Tooltip("Text inside the panel that lists the item names.")]
    [SerializeField] private TMP_Text itemListText;

    private void Start()
    {
        menuAction.action.Enable();
        inventoryPanel.SetActive(false);
        SetCursorFree(false);   // start the game in normal play mode
    }

    private void Update()
    {
        if (menuAction.action.WasPressedThisFrame())
        {
            SetCursorFree(!CursorFree);
        }
        // In menu mode, clicking somewhere that isn't UI (the game world) goes back to playing.
        else if (CursorFree && Mouse.current.leftButton.wasPressedThisFrame
                 && !EventSystem.current.IsPointerOverGameObject())
        {
            SetCursorFree(false);
        }
    }

    // Called by the Inventory button (hooked up in the button's On Click in the Inspector).
    // Opening shows the item list; closing goes straight back to playing.
    public void ToggleInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            SetCursorFree(false);   // also closes the panel
        }
        else
        {
            inventoryPanel.SetActive(true);
            RefreshList();
        }
    }

    // Rewrites the panel text with one item name per line.
    private void RefreshList()
    {
        var items = InventoryManager.Instance.Items;
        itemListText.text = items.Count == 0 ? "(empty)" : string.Join("\n", items);
    }

    // Switches between play mode (cursor hidden and locked) and menu mode (cursor visible).
    private void SetCursorFree(bool free)
    {
        CursorFree = free;
        Cursor.lockState = free ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = free;

        if (!free) inventoryPanel.SetActive(false);   // close the panel when going back to playing
    }
}
