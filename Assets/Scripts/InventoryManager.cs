// Elise Li, Sophia Cho
// ============================================================================
// InventoryManager.cs
// Keeps track of the items the player is carrying. There is only one in the
// scene; other scripts reach it through InventoryManager.Instance.
// ============================================================================

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Shared access point so any script can call InventoryManager.Instance.AddItem(...)
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory")]
    [Tooltip("Items the player is holding. Shown here so you can watch it change while testing.")]
    [SerializeField] private List<string> items = new List<string>();

    // Read-only view of the items, so other scripts (like InventoryUI) can display them.
    public IReadOnlyList<string> Items => items;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Picked up: " + itemName);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        items.Remove(itemName);
    }
}
